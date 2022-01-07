namespace ROELibrary
{
    public interface ICarDevice
    {
        //config
        void setSpeed(uint speed);
        void setRotationalSpeed(uint speed);


        //actions
        void goForward(uint distance);
        void rotate(uint angle);
    }
}
