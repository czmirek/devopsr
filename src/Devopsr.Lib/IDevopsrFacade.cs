using Devopsr . Lib . Services . Project . Interfaces;

namespace Devopsr . Lib;

public interface IDevopsrFacade
    {
    IProjectService ProjectService { get; }
    }