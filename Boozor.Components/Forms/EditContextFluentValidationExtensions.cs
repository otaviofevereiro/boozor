﻿using Boozor.Core;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;

namespace Boozor.Components.Forms
{
    public static class EditContextFluentValidationExtensions
    {
        public static EditContext AddFluentValidation(this EditContext editContext)
        {
            if (editContext == null)
                throw new ArgumentNullException(nameof(editContext));

            var messages = new ValidationMessageStore(editContext);

            editContext.OnValidationRequested += (sender, eventArgs) => ValidateModel((EditContext)sender, messages);
            editContext.OnFieldChanged += (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier);

            return editContext;
        }

        private static void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {
            var validatableEntity = editContext.Model as IValidatableObject;

            if (validatableEntity == null)
                throw new InvalidCastException($"O model {editContext.Model.GetType().Name} não pode ser validado, para isso é necessário herdar do tipo Entity<TEntity>");

            var validator = validatableEntity.Validator;
            var validationResults = validator.Validate(editContext.Model);

            messages.Clear();
            foreach (var validationResult in validationResults.Errors)
            {
                messages.Add(editContext.Field(validationResult.PropertyName), validationResult.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
        {
            var validatableEntity = fieldIdentifier.Model as IValidatableObject;

            if (validatableEntity == null)
                return;

            var properties = new[] { fieldIdentifier.FieldName };
            var context = new ValidationContext(fieldIdentifier.Model, new PropertyChain(), new MemberNameValidatorSelector(properties));
            var validator = validatableEntity.Validator;
            var validationResults = validator.Validate(context);

            messages.Clear(fieldIdentifier);

            foreach (var error in validationResults.Errors.Select(error => error.ErrorMessage))
            {
                messages.Add(fieldIdentifier, error);
            }

            editContext.NotifyValidationStateChanged();
        }
    }
}
