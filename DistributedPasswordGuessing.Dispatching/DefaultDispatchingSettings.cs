namespace DistributedPasswordGuessing.Dispatching
{
    #region

    using System;

    #endregion

    /// <summary>
    /// Стандартные параметры для диспечеризации.
    /// </summary>
    public static class DefaultDispatchingSettings
    {
        #region Constants

        /// <summary>
        /// Стандартный путь к сверткам для подбора.
        /// </summary>
        public const string ConvolutionsPath = "default.conv";

        /// <summary>
        /// Стандартное количество слов для подбора.
        /// </summary>
        public const long DefaultNumberOfWordsThatNeedToBeIterated = 100000;

        /// <summary>
        /// Стандартное количество поддерживаемых заданий в очереди заданий.
        /// </summary>
        public const int MinCountTaskInQueue = 50;

        #endregion

        #region Static Fields

        /// <summary>
        /// Стандартное время для обработки заданий.
        /// </summary>
        public static readonly TimeSpan MaxTimeForTask = new TimeSpan(0, 0, 20, 0);

        #endregion
    }
}