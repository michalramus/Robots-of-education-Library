namespace ROELibrary
{
    public class CarModel : VRobotModel
    {
        //properties
        public virtual uint? id { get; set; } = null;

        /// <summary>
        /// [en1][en2][en3][en4][speedControl-analogPin]
        /// </summary>
        /// <value></value>
        public virtual uint[] pins { get; set; } = null;

        //calibration properties
        /// <summary>
        /// impulses sent by hall sensor per one wheel rotation
        /// </summary>
        /// <value></value>
        public virtual uint? impulsesPerRotation { get; set; } = null;

        /// <summary>
        /// circumference of wheel in centimeters
        /// </summary>
        /// <value></value>
        public virtual uint? circumference { get; set; } = null;

        internal override ERobotsSymbols getModelType()
        {
            return ERobotsSymbols.car;
        }

    /// <summary>
    /// return null when id not set
    /// </summary>
    /// <returns></returns>
        internal override uint? getID()
        {
            return id;
        }
    }
}
