namespace ROELibrary
{
    public interface ICarDevice
    {
        //config
        void setSpeed(int speed);
        void setRotationalSpeed(int speed);


        //actions
        void goForward(int distance);
        void rotate(int angle);
    }
}
