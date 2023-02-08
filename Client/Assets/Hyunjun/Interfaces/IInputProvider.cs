using Enums;

namespace Interfaces
{
    /// <summary>
    /// �Է� üũ �����ִ� �������̽�
    /// </summary>
    public interface IInputProvider
    {
        //���� GetAxis�� ������ ��� 
        public float GetAxis(Axis axis);
        //���� �������� üũ
        public bool GetActionPressed(InputAction inputAction);
    }
}
