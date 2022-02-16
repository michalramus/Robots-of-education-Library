namespace ROELibrary
{
    public interface ICarDevice : IRobotDevice
    {
        //CONFIG

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed">in precents</param>
        void setSpeed(uint speed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed">speed in precents</param>
        void setRotationalSpeed(uint speed);


        //ACTIONS

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
