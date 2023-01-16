using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class ObjectField : BaseField<AsteroidVariables>, IEventHandler, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler, IResolvedStyle, IBindable, INotifyValueChanged<AsteroidVariables>, IMixedValueSupport
{
    public ObjectField(string label, VisualElement visualInput) : base(label, visualInput) {
    }
    public Type objectType { get; set; }
}
