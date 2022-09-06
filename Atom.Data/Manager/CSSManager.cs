using Atom.Data.Criteria.Security;
using Atom.Data.Entity.CSS;
using Atom.Data.Model.CSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atom.Data.Manager
{
    public class CSSManager : ManagerBase, ICSSManager
    {
        #region Constructors
        public CSSManager(IServiceProvider provider) : base(provider) { }
        #endregion

        #region CSS
        public IEnumerable<CSSRuleModel?> GetCSS(int userId)
        {
            GetCSSCriteria criteria = new() { CSSUserId = userId };
            return base.ReadMany<CSSRuleModel, CSSRuleEntity, GetCSSCriteria>(criteria);
        }


        public void UpdateCSS(CSSRuleModel cssRuleModel)
        {
            base.UpdateSingle<CSSRuleModel, CSSRuleEntity>(cssRuleModel);
        }

        public void CreateCSS(IEnumerable<CSSRuleModel> cssRuleModel)
        {
            base.CreateMany<CSSRuleModel, CSSRuleEntity>(cssRuleModel);
        }

        public void UpdateManyCSS(IEnumerable<CSSRuleModel> cssRuleModel)
        {
            base.UpdateMany<CSSRuleModel, CSSRuleEntity>(cssRuleModel);
        }
        #endregion

    }
}
