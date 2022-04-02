export const generateFormData = (formValues: Record<string, any>): FormData => {
	const formData = new FormData();
	const bindForm = (form: FormData, fieldValue: string, value: any) => {
		if (value instanceof File) {
			form.append(fieldValue, value);
		} else if (value instanceof Object) {
			form.append(fieldValue, JSON.stringify(value));
		} else {
			form.append(fieldValue, value);
		}
	};
	Object.keys(formValues).forEach((key: string, index) => {
		const fieldValue = formValues[key];
		if (fieldValue == null) return;
		if (fieldValue instanceof Array) {
			if (fieldValue[0] instanceof File) {
				fieldValue.forEach((value) => {
					bindForm(formData, key, value);
				});
			} else {
				bindForm(formData, key, JSON.stringify(fieldValue));
			}
		} else {
			bindForm(formData, key, fieldValue);
		}
	});
	return formData;
};
