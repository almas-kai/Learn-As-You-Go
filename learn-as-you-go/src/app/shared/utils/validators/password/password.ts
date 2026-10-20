import { RootFieldContext, SchemaPath, validate } from '@angular/forms/signals';
import { PasswordValidatorConfigurationItem } from './types';
import { DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS } from './default-configuration/default-configuration';

/*
  ? We could make an endpoint that will send an actual password requirements from BE side. So that we have only one single source of truth. But password requirements won't change frequently. The single-source of truth is a very sweet idea, we will implement in future. Maybe, we need to discuss it first.
*/
export function password(path: SchemaPath<string>): void {
  for(const [kind, rule] of (Object.entries(DEFAULT_PASSWORD_VALIDATOR_CONFIGURATIONS) as [string, PasswordValidatorConfigurationItem][])) {
    if(!rule.isOn) {
      continue;
    }

    validate(path, (context: RootFieldContext<string>) => {
      return rule.isValidPassword(context.value())
        ? null
        : { kind, message: rule.message, params: rule.params }
    });
  }
}