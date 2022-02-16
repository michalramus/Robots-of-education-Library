namespace ROELibrary
{
    public class CarModel : VRobotModel
    {
        //TODO: set every property to null and public

        //properties
        internal virtual uint? id { get; set; } = 0;

        /// <summary>
        /// [en1][en2][en3][en4][speedControl-analogPin]
        /// </summary>
        /// <value></value>
        internal virtual uint[] pins { get; set; } = { 1, 2, 3, 4, 5 };

        //calibration properties
        /// <summary>
        /// impulses sent by hall sensor per one wheel rotation
        /// </summary>
        /// <value></value>
        internal virtual uint? impulsesPerRotation { get; set; } = 50;

        /// <summary>
        /// circumference of wheel in centimeters
        /// </summary>
        /// <value></value>
        internal virtual uint? circumference { get; set; } = 30;

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
