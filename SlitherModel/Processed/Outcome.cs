namespace SlitherModel.Processed
{
    // base outcome rating on not dying as priority, then getting kills and growth
    public class Outcome
    {
        /// <summary>
        /// 1 second
        /// </summary>
        public OutcomeScore ImmediateTerm { get; set; }

        /// <summary>
        /// 10 seconds
        /// </summary>
        public OutcomeScore ShortTerm { get; set; }

        /// <summary>
        /// 60 seconds
        /// </summary>
        public OutcomeScore LongTerm { get; set; }

        /// <summary>
        /// until death
        /// </summary>
        public OutcomeScore LifeTerm { get; set; }
    }
}
