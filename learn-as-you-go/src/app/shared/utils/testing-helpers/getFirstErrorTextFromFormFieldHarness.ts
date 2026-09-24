import { MatErrorHarness, MatFormFieldHarness } from '@angular/material/form-field/testing';

export async function getFirstErrorTextFromFormFieldHarness(formField: MatFormFieldHarness): Promise<string> {
  const errorHarnesses: MatErrorHarness[] = await formField.getErrors();

  if(errorHarnesses.length === 0) {
    return '';
  }
  
  return await errorHarnesses[0].getText();;
}
