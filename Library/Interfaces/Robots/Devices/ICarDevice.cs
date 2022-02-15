namespace ROELibrary
{
    public interface ICarDevice : IRobotDevice
    {
        //config
        void setSpeed(uint speed);
        void setRotationalSpeed(uint speed);


        //actions
        void goForward(uint distance);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="direction">0 - turn left; 1 - turn right</param>
        void rotate(uint angle, bool direction);
    }
}
