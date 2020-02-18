using API_AGROMG.SimpleforDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Dtos
{
    public class UserPermissionLanguageDtos: UserPermissionDto
    {
        public List<LangcontentDto> LanguageContent { get; set; }
    }
}
