export const Endpoints = {
  account: {
    register: '/register',
    login: '/login',
    refresh: '/refresh',
    confirmEmail: '/confirmEmail',
    resendConfirmationEmail: '/resendConfirmationEmail',
    forgotPassword: '/forgotPassword',
    resetPassword: '/resetPassword',
    manage2fa: '/manage/2fa',
    manageInfo: '/manage/info'
  }
} as const;