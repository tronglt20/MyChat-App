using System.ComponentModel;

namespace Domain.Enums
{
    public enum UserRegistrationStatusEnum : byte
    {
        [Description("Đã mở")]
        Open = 1,

        [Description("Đã huỷ")]
        Canceled = 2,

        [Description("Đã đăng ký")]
        Registrated = 3,
    }
}
