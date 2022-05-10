using System;
using FluiTec.AppFx.Identity.Data;

namespace FluiTec.AppFx.Identity.IdentityStores; 

/// <summary>
///     An identity store.
/// </summary>
public abstract class IdentityStore : IDisposable
{
    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dataService">  The data service. </param>
    protected IdentityStore(IIdentityDataService dataService)
    {
        UnitOfWork = dataService.BeginUnitOfWork() ?? throw new ArgumentNullException(nameof(dataService));
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Gets the unit of work.
    /// </summary>
    /// <value>
    ///     The unit of work.
    /// </value>
    protected IIdentityUnitOfWork UnitOfWork { get; }

    #endregion

    #region IDisposable

    /// <summary>
    ///     True if disposed.
    /// </summary>
    private bool _disposed;

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    ///     resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    ///     Releases the unmanaged resources used by the IdentityStore and optionally releases the
    ///     managed resources.
    /// </summary>
    /// <param name="disposing">
    ///     True to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            UnitOfWork.Commit();
        _disposed = true;
    }

    #endregion
}