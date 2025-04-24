using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record RelatedPersonDto(RelationType Type, int RelatedPersonId);