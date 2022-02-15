namespace ROELibrary
{
    public interface ICarDevice : IRobotDevice
    {
        //config
        void setSpeed(uint speed);
        void setRotationalSpeed(uint speed);


        //actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance">unit is centimeters</param>
        void goForward(uint distance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance">unit is centimeters</param>
        void goBackward(uint distance);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle">unit is degrees</param>
        /// <param name="direction">0 - turn left; 1 - turn right</param>
        void rotate(uint angle, bool direction);
    }
}
