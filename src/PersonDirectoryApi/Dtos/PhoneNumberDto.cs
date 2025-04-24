using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PhoneNumberDto(PhoneNumberType Type, string Number);