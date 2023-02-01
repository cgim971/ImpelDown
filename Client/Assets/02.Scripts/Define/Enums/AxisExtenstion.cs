namespace Enums
{
    public enum Axis
    {
        X,
        Y
    }

    public static class AxisExtenstion
    {
        //enum을 string으로 바꿔주는 역할
        public static string ToUnityAxis(this Axis axis)
        {
            return axis == Axis.X ? "Horizontal" : "Vertical";
        }
    }
}
