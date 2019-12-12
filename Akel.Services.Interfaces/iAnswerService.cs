using Akel.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Services.Interfaces
{
    public interface iAnswerService
    {
        IEnumerable<Answer> Get();
        IEnumerable<Answer> GetById(Guid id);
    }
}
