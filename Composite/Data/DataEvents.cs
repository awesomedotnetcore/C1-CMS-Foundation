﻿using Composite.Core.Implementation;


namespace Composite.Data
{
    /// <summary>
    /// This class contains all the event fired by Composite C1 when changes are made to data items. 
    /// 
    /// Use <see cref="Composite.Data.DataEvents&lt;TData&gt;.OnStoreChanged"/> to catch any data change event, including events originating from other servers in a load balance setup
    /// or changes made directly to a store (which Composite C1 can detect). This event do not contain details about the specific data item changed and is raised after the fact.
    /// 
    /// Use the more detailed operations to catch data events that happen in the current website process. The 'OnBefore' events enable you to manipulate data before they are stored. 
    /// The 'OnAfter' events let you react to data changes in detail, for instance updating a cache.
    /// 
    /// A combination of <see cref="Composite.Data.DataEvents&lt;TData&gt;.OnStoreChanged"/> and the detailed data events can be used to create a highly optimized cache.
    /// </summary>
    /// <example>
    /// <code>
    /// void MyMethod()
    /// {
    ///    DataEvents&lt;IMyDataType&gt;.OnBeforeAdd += new DataEventHandler(DataEvents_OnBeforeAdd);
    ///    DataEvents&lt;IMyDataType&gt;.OnStoreChanged += new StoreEventHandler(DataEvents_OnStoreChanged);
    ///    
    ///    using (DataConnection connection = new DataConnection())
    ///    {
    ///       IMyDataType myDataType = DataConnection.New&lt;IMyDataType&gt;();
    ///       myDataType.Name = "Foo";
    ///       
    ///       connection.Add&lt;IMyDataType&gt;(myDataType); // This will fire both of the the events in the local process!
    ///       // if other servers share data store with this site they will see OnStoreChanged fire.
    ///    }
    /// }
    /// 
    /// 
    /// void DataEvents_OnBeforeAdd(object sender, DataEventArgs dataEventArgs)
    /// {        
    ///     // here a minor update to the cache could be done (like adding info about the new element only).
    /// }
    /// 
    /// 
    /// void DataEvents_OnStoreChanged(object sender, StoreEventArgs storeEventArgs)
    /// {        
    ///     if (!storeEventArgs.DataEventsFired)
    ///     {
    ///         // an external update event happened - DataEvents_OnBeforeAdd not fired
    ///         // here a complete cache flush could be done
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TData">Data type to attach events to</typeparam>
    public static class DataEvents<TData>
        where TData : class, IData
    {
        /// <summary>
        /// This event is fired just before a data item is added to the C1 store.
        /// See <see cref="Composite.Data.DataConnection.Add&lt;TData&gt;(TData)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnBeforeAdd += new DataEventHandler(DataEvents_OnBeforeAdd);
        ///    
        ///    using (DataConnection connection = new DataConnection())
        ///    {
        ///       IMyDataType myDataType = DataConnection.New&lt;IMyDataType&gt;();
        ///       myDataType.Name = "Foo";
        ///       
        ///       connection.Add&lt;IMyDataType&gt;(myDataType); // This will fire the event!
        ///    }
        /// }
        /// 
        /// 
        /// void DataEvents_OnBeforeAdd(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnBeforeAdd
        {
            add
            {                
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnBeforeAdd += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnBeforeAdd -= value;
            }
        }



        /// <summary>
        /// This event is fired just after a data item has been added to the C1 store.
        /// See <see cref="Composite.Data.DataConnection.Add&lt;TData&gt;(TData)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnAfterAdd += new DataEventHandler(DataEvents_OnAfterAdd);
        ///    
        ///    using (DataConnection connection = new DataConnection())
        ///    {
        ///       IMyDataType myDataType = DataConnection.New&lt;IMyDataType&gt;();
        ///       myDataType.Name = "Foo";
        ///       
        ///       connection.Add&lt;IMyDataType&gt;(myDataType); // This will fire the event!
        ///    }
        /// }
        /// 
        /// 
        /// void DataEvents_OnAfterAdd(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnAfterAdd
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnAfterAdd += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnAfterAdd -= value;
            }
        }



        /// <summary>
        /// This event is fired just before a data item is updated in the C1 store.
        /// See <see cref="Composite.Data.DataConnection.Update&lt;TData&gt;(TData)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnBeforeUpdate += new DataEventHandler(DataEvents_OnBeforeUpdate);
        ///    
        ///    using (DataConnection connection = new DataConnection())
        ///    {
        ///       IMyDataType myDataType = 
        ///          (from item in connection.get&lt;IMyDataType&gt;()
        ///           where item.Id == 1
        ///           select item).First();
        ///           
        ///       myDataType.Name = "Foo";
        ///       
        ///       connection.Update&lt;IMyDataType&gt;(myDataType); // This will fire the event!
        ///    }
        /// }
        /// 
        /// 
        /// void DataEvents_OnBeforeUpdate(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnBeforeUpdate
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnBeforeUpdate += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnBeforeUpdate -= value;
            }
        }



        /// <summary>
        /// This event is fired just after a data item has been added in the C1 store.
        /// See <see cref="Composite.Data.DataConnection.Update&lt;TData&gt;(TData)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnAfterUpdate += new DataEventHandler(DataEvents_OnAfterUpdate);
        ///    
        ///    using (DataConnection connection = new DataConnection())
        ///    {
        ///       IMyDataType myDataType = 
        ///          (from item in connection.get&lt;IMyDataType&gt;()
        ///           where item.Id == 1
        ///           select item).First();
        ///           
        ///       myDataType.Name = "Foo";
        ///       
        ///       connection.Update&lt;IMyDataType&gt;(myDataType); // This will fire the event!
        ///    }
        /// }
        /// 
        /// 
        /// void DataEvents_OnAfterUpdate(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnAfterUpdate
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnAfterUpdate += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnAfterUpdate -= value;
            }
        }



        /// <summary>
        /// This event is fired just before a data item is deleted from the C1 store.
        /// See <see cref="Composite.Data.DataConnection.Delete&lt;TData&gt;(TData)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnDeleted+= new DataEventHandler(DataEvents_OnDeleted);
        ///    
        ///    using (DataConnection connection = new DataConnection())
        ///    {
        ///       IMyDataType myDataType = 
        ///          (from item in connection.get&lt;IMyDataType&gt;()
        ///           where item.Id == 1
        ///           select item).First();
        ///           
        ///       connection.Delete&lt;IMyDataType&gt;(myDataType); // This will fire the event!
        ///    }
        /// }
        /// 
        /// 
        /// void DataEvents_OnDeleted(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnDeleted
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnDeleted += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnDeleted -= value;
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event StoreEventHandler OnStoreChanged
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnStoreChanged += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnStoreChanged -= value;
            }
        }



        /// <summary>
        /// This event is fired just after a new data item is created.
        /// See <see cref="Composite.Data.DataConnection.New&lt;TData&gt;()"/>
        /// </summary>
        /// <example>
        /// <code>
        /// void MyMethod()
        /// {
        ///    DataEvents&lt;IMyDataType&gt;.OnNew += new DataEventHandler(DataEvents_OnNew);
        ///    
        ///    IMyDataType myDataType = DataConnection.New&lt;IMyDataType&gt;(); // This will fire the event!
        /// }
        /// 
        /// 
        /// void DataEvents_OnNew(object sender, DataEventArgs dataEventArgs)
        /// {        
        /// }
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "We had to be backwards compatible")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "We had to be backwards compatible")]
        public static event DataEventHandler OnNew
        {
            add
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnNew += value;
            }
            remove
            {
                ImplementationFactory.CurrentFactory.CreateStatelessDataEvents<TData>().OnNew -= value;
            }
        }
    }
}
