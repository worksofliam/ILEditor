using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.WPFHelpers
{
    /**
    *  This was included with some other code I got:  http://blog.thekieners.com/2010/09/08/relativesource-binding-with-findancestor-mode-in-silverlight/
    */
    public abstract class ViewModelBase : INotifyPropertyChanged
    {


        /// <summary>
        /// Internal store for the property values.
        /// </summary>
        private Dictionary<string, object> values;

        public ViewModelBase()
        {
            this.values = new Dictionary<string, object>();
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyInfo">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                // root event source first
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }

        #endregion

        // Unfortunately, C# currently does not support property delegates, in other words, a reference to a property. 
        private PropertyInfo GetPropertyInfo(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            var constantExpression = memberExpression.Expression as ConstantExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo;
        }

        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {
            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("Invalid view model property definition.");

            PropertyInfo propertyInfo = this.GetPropertyInfo(lambda);

            T existingValue = GetValueInternal<T>(propertyInfo.Name);

            if (!object.Equals(existingValue, value))
            {
                this.values[propertyInfo.Name] = value;
                this.OnPropertyChanged(propertyInfo.Name);
            }
        }

        /// <summary>
        /// This very will allways fire OnPropertyChanged
        ///  - Should not be used unless you know what your doing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected void SetValueAllwaysNotify<T>(Expression<Func<T>> property, T value)
        {
            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("Invalid view model property definition.");

            PropertyInfo propertyInfo = this.GetPropertyInfo(lambda);

            T existingValue = GetValueInternal<T>(propertyInfo.Name);

            this.values[propertyInfo.Name] = value;
            this.OnPropertyChanged(propertyInfo.Name);
        }




        protected T GetValue<T>(Expression<Func<T>> property)
        {
            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("Invalid view model property definition.");

            PropertyInfo propertyInfo = this.GetPropertyInfo(lambda);

            T value = GetValueInternal<T>(propertyInfo.Name);

            // auto create instance for common list types
            if (value == null && !propertyInfo.CanWrite)
            {
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                {
                    value = (T)Activator.CreateInstance(propertyInfo.PropertyType);
                }
                else if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    value = (T)Activator.CreateInstance(propertyInfo.PropertyType);
                }
                else if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    Type observableCollectionType = typeof(ObservableCollection<>).MakeGenericType(propertyInfo.PropertyType.GetGenericArguments());
                    value = (T)Activator.CreateInstance(observableCollectionType);
                }
                else if (propertyInfo.PropertyType == typeof(System.Collections.IEnumerable))
                {
                    Type observableCollectionType = typeof(ObservableCollection<>).MakeGenericType(typeof(object));
                    value = (T)Activator.CreateInstance(observableCollectionType);
                }

                this.values[propertyInfo.Name] = value;
            }

            return value;

        }

        internal T GetValueInternal<T>(string propertyName)
        {
            object value;
            if (values.TryGetValue(propertyName, out value))
                return (T)value;
            else
                return default(T);
        }

    }// end of view model base class


}
