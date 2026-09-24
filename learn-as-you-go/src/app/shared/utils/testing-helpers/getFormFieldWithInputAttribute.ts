import { HarnessPredicate } from '@angular/cdk/testing';
import { MatFormFieldHarness } from '@angular/material/form-field/testing';
import { MatInputHarness } from '@angular/material/input/testing';

export function getFormFieldHarnessPredicateWithInputAttribute(attr: string, value: string): HarnessPredicate<MatFormFieldHarness> {
  return MatFormFieldHarness.with({}).add(`input[${attr}="${value}"]`, async (formField) => {
    const control = await formField.getControl() as MatInputHarness | null;
    return await (await control?.host())?.getAttribute(attr) === value;
  });
}