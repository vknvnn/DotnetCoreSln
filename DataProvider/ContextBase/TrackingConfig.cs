using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ContextBase
{
    public class TrackingConfig
    {
        private IList<string> _trackings;
        public static TrackingConfig Tracking { get { return new TrackingConfig(); } }

        public TrackingConfig()
        {
            _trackings = new List<string>();
        }

        public TrackingConfig Map<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> action)
        {
            Type type = typeof(TSource);
            MemberExpression body = action.Body as MemberExpression;
            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)action.Body;
                body = ubody.Operand as MemberExpression;
            }
            this._trackings.Add(body.Member.Name);
            return this;
        }
    }
}
