namespace ROELibrary
{
    public interface ICarDevice : IRobotDevice
    {
        //config
        void setSpeed(uint speed);
        void setRotationalSpeed(uint speed);


        //actions
        void goForward(uint distance);
        void rotate(uint angle);
    }
}
