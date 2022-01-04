namespace ROELibrary
{
    public class CarModel : VRobotModel
    {
        //properties
        public int? id { get; set; }

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
        public decimal? carGoTime { get; set; }

        /// <summary>
        /// time when car turn 90 degrees
        /// </summary>
        /// <value></value>
        public decimal? carTurnTime { get; set; }

        internal override ERobotsSymbols getModelType()
        {
            return ERobotsSymbols.car;
        }
    }
}
