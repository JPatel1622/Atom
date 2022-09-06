using Atom.Data.Model.CSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Manager
{
    public interface ICSSManager
    {
        #region CSS
        public void UpdateCSS(CSSRuleModel cssRuleModel);

        public IEnumerable<CSSRuleModel> GetCSS(int userId);

        public void CreateCSS(IEnumerable<CSSRuleModel> cssRules);

        public void UpdateManyCSS(IEnumerable<CSSRuleModel> cssRules);

        #endregion
    }
}
