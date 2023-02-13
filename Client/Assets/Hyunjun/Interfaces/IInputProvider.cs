using Enums;

namespace Interfaces
{
    /// <summary>
    /// 입력 체크 도와주는 인터페이스
    /// </summary>
    public interface IInputProvider
    {
        //기존 GetAxis랑 동일한 기능 
        public float GetAxis(Axis axis);
        //지금 눌렀는지 체크
        public bool GetActionPressed(InputAction inputAction);
    }
}
