namespace ROELibrary
{
    public class CarModel : VRobotModel
    {
        //properties
        public int? id { get; set; } = null;

        /// <summary>
        /// [en1][en2][en3][en4][speedControl-analogPin]
        /// </summary>
        /// <value></value>
        public int[] pins { get; set; } = new int[5];

        //calibration properties
        /// <summary>
        /// time when car ride 1 meter
        /// </summary>
        /// <value></value>
        public decimal? carGoTime { get; set; } = null;

        /// <summary>
        /// time when car turn 90 degrees
        /// </summary>
        /// <value></value>
        public decimal? carTurnTime { get; set; } = null;

        internal override ERobotsSymbols getModelType()
        {
            return ERobotsSymbols.car;
        }

    /// <summary>
    /// return null when id not set
    /// </summary>
    /// <returns></returns>
        internal override int? getID()
        {
            return id;
        }
    }
}
