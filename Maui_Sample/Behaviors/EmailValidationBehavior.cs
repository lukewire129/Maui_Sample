﻿using CommunityToolkit.Maui.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maui_Sample.Behaviors
{
    class EmailValidationBehavior :Behavior<Entry>
    {
                const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

                public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidationBehavior), false);

                public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

                public bool IsValid
                {
                        get { return (bool)base.GetValue(IsValidProperty); }
                        private set { base.SetValue(IsValidPropertyKey, value); }
                }

                protected override void OnAttachedTo(Entry bindable)
                {
                        bindable.TextChanged += HandleTextChanged;
                }

                void HandleTextChanged(object sender, TextChangedEventArgs e)
                {
                        IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                        ((Entry)sender).TextColor = IsValid ? Colors.Black : Colors.Red;
                }

                protected override void OnDetachingFrom(Entry bindable)
                {
                        bindable.TextChanged -= HandleTextChanged;

                }
        }
}
