
export const VALIDATION_MESSAGES: any = {
  required: () => 'This field is required',
  email: () => 'Invalid email address',

};

export function getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
  const config = VALIDATION_MESSAGES[validatorName];
  return config ? config(validatorValue) : 'Invalid input';
}