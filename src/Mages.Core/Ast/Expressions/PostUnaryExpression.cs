﻿namespace Mages.Core.Ast.Expressions
{
    /// <summary>
    /// Base class for all post unary expressions.
    /// </summary>
    public abstract class PostUnaryExpression : ComputingExpression, IExpression
    {
        #region Fields

        private readonly IExpression _value;

        #endregion

        #region ctor

        public PostUnaryExpression(IExpression value, TextPosition end)
            : base(value.Start, end)
        {
            _value = value;
        }

        #endregion

        #region Properties

        public IExpression Value 
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public void Accept(ITreeWalker visitor)
        {
            visitor.Visit(this);
        }

        public virtual void Validate(IValidationContext context)
        {
            if (_value is EmptyExpression)
            {
                var error = new ParseError(ErrorCode.OperandRequired, _value);
                context.Report(error);
            }
            else
            {
                _value.Validate(context);
            }
        }

        #endregion

        #region Operations

        public sealed class Factorial : PostUnaryExpression
        {
            public Factorial(IExpression expression, TextPosition end)
                : base(expression, end)
            {
            }
        }

        public sealed class Transpose : PostUnaryExpression
        {
            public Transpose(IExpression expression, TextPosition end)
                : base(expression, end)
            {
            }
        }

        public sealed class Increment : PostUnaryExpression
        {
            public Increment(IExpression expression, TextPosition end)
                : base(expression, end)
            {
            }

            public override void Validate(IValidationContext context)
            {
                if (Value is VariableExpression == false)
                {
                    var error = new ParseError(ErrorCode.IncrementOperand, Value);
                    context.Report(error);
                }
                else
                {
                    base.Validate(context);
                }
            }
        }

        public sealed class Decrement : PostUnaryExpression
        {
            public Decrement(IExpression expression, TextPosition end)
                : base(expression, end)
            {
            }

            public override void Validate(IValidationContext context)
            {
                if (Value is VariableExpression == false)
                {
                    var error = new ParseError(ErrorCode.DecrementOperand, Value);
                    context.Report(error);
                }
                else
                {
                    base.Validate(context);
                }
            }
        }

        #endregion
    }
}
