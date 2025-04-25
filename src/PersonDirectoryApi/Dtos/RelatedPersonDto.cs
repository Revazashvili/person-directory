using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record RelatedPersonDto(RelationType Type, string RelatedPersonPersonalNumber);