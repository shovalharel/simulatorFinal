﻿#pragma checksum "..\..\RunControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "824F345EFAE16DBADDEB1704151197F03176B2473829ADB5139BBCC64FCA78C0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using simulator;


namespace simulator {
    
    
    /// <summary>
    /// RunControl
    /// </summary>
    public partial class RunControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\RunControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Open_csv_file;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\RunControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Open_csv2_file;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\RunControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Open_xml_file;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\RunControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Start;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/simulator;component/runcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RunControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Open_csv_file = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\RunControl.xaml"
            this.Open_csv_file.Click += new System.Windows.RoutedEventHandler(this.Open_csv_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Open_csv2_file = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\RunControl.xaml"
            this.Open_csv2_file.Click += new System.Windows.RoutedEventHandler(this.Open_csv2_file_click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Open_xml_file = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\RunControl.xaml"
            this.Open_xml_file.Click += new System.Windows.RoutedEventHandler(this.Open_xml_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Start = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\RunControl.xaml"
            this.Start.Click += new System.Windows.RoutedEventHandler(this.Start_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

