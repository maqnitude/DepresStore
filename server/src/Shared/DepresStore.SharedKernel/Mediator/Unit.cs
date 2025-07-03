namespace DepresStore.SharedKernel.Mediator
{
    /// <summary>
    /// Represents a void type, since <see cref="System.Void"/> is not a valid type in C#.
    /// Reference: https://github.com/jbogard/MediatR/blob/master/src/MediatR.Contracts/Unit.cs
    /// </summary>
    public readonly struct Unit
    {
        private static readonly Unit _value = new();

        public static ref readonly Unit Value => ref _value;
    }
}