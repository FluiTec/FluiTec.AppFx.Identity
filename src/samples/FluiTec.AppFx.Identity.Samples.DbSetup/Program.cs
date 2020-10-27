using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using FluiTec.AppFx.Identity.Data;
using FluiTec.AppFx.Identity.Data.Entities;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Identity.Samples.DbSetup
{
    internal class Program
    {
        /// <summary>   Main entry-point for this application. </summary>
        /// <param name="args"> An array of command-line argument strings. </param>
        private static void Main(string[] args)
        {
            // load config from json
            var path = GetApplicationRoot();
            Console.WriteLine($"BasePath: {path}");
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.secret.json", false, true)
                .Build();

            // configure
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();
            services.ConfigureDynamicIdentityDataProvider(manager);

            // finish di
            var sp = services.BuildServiceProvider();

            var dataService = sp.GetRequiredService<IIdentityDataService>();
            using (var uow = dataService.BeginUnitOfWork())
            {
                //CreateSampleData(uow);

                var user = uow.UserRepository.GetAll().First();
                var claims = uow.UserRepository.FindAllClaims(user);
            }
        }

        /// <summary>   Creates sample data. </summary>
        /// <param name="uow">  The uow. </param>
        private static void CreateSampleData(IIdentityUnitOfWork uow)
        {
            var role = uow.RoleRepository.Add(new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "RoleName"
            });

            var roleClaim = uow.RoleClaimRepository.Add(new RoleClaimEntity
            {
                RoleId = role.Id,
                Type = "rClaimType",
                Value = "rClaimValue"
            });

            var user = uow.UserRepository.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "m.mustermann@musterfirma.de",
                Name = "m.mustermann@musterfirma.de",
                FullName = "Max Mustermann",
                EmailConfirmed = false,
                AccessFailedCount = 0,
                LockedOutPermanently = false,
                LockedOutTill = null,
                LockoutEnabled = false,
                PasswordHash = "<>",
                SecurityStamp = "<>",
                TwoFactorEnabled = false,
                Phone = "<>",
                PhoneConfirmed = false
            });

            var userClaim = uow.UserClaimRepository.Add(new UserClaimEntity
            {
                UserId = user.Id,
                Type = "uClaimType",
                Value = "uClaimValue"
            });

            var userRole = uow.UserRoleRepository.Add(new UserRoleEntity
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            uow.Commit();
        }

        /// <summary>   Gets application root. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        /// <returns>   The application root. </returns>
        private static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath ?? throw new InvalidOperationException()).Value;
                return appRoot;
            }
            else
            {
                var appPathMatcher = new Regex(@"(?<!file)\/+[\S\s]*?(?=\/+bin)");
                var appRoot = appPathMatcher.Match(exePath ?? throw new InvalidOperationException()).Value;
                return appRoot;
            }
        }
    }
}