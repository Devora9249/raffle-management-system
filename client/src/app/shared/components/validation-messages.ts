// src/app/shared/utils/validation-messages.ts

export const VALIDATION_MESSAGES: any = {
  required: () => 'This field is required',
  email: () => 'Invalid email address',
  minlength: (args: any) => `Must be at least ${args.requiredLength} characters long`,
  maxlength: (args: any) => `Cannot exceed ${args.requiredLength} characters`,
  pattern: () => 'Invalid format',
};

export function getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
  const config = VALIDATION_MESSAGES[validatorName];
  return config ? config(validatorValue) : 'Invalid input';
}