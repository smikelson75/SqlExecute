namespace SqlExecute.Engine.Actions.Abstractions
{
    /// <summary>
    /// Represents the status of an action.
    /// </summary>
    public enum ActionStatus
    {
        /// <summary>
        /// The action is pending and has not started yet.
        /// </summary>
        Pending,

        /// <summary>
        /// The action has started and is currently in progress.
        /// </summary>
        Started,

        /// <summary>
        /// The action has completed successfully.
        /// </summary>
        Complete,

        /// <summary>
        /// The action has been cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The action has failed.
        /// </summary>
        Failed
    }
}