import React, { createElement, ReactElement, useImperativeHandle } from "react";
import { DeepPartial, SubmitHandler, useForm, FormProvider } from "react-hook-form";
import { DefaultValues } from "react-hook-form/dist/types/form";
import { Resolver } from "react-hook-form/dist/types/resolvers";

interface IFormProps {
	onSubmit: (formValues: any) => void;
	defaultValues?: DefaultValues<any>;
	className?: string;
	resolver?: Resolver<any>;
}
export interface BaseFormRef {
	getValues: () => Record<string, any>;
	setValue: (key: string, value: any) => void;
	setValues: (records: Record<string, any>) => void;
}

type IProps<T> = IFormProps & { children: ReactElement | ReactElement[] };
type IForwardRef<T> = React.ForwardedRef<BaseFormRef>;

function BaseForm<T>(props: IProps<T>, ref: IForwardRef<T>) {
	const { defaultValues, children, onSubmit, className, resolver } = props;
	const methods = useForm({ defaultValues, resolver });
	const { handleSubmit, getValues, setValue, reset } = methods;

	useImperativeHandle(
		ref,
		() => ({
			getValues,
			setValue,
			setValues: reset
		}),
		[getValues, reset, setValue]
	);

	return (
		<FormProvider {...methods}>
			{/*<button onClick={() => console.log('getValues()',getValues())}>Inside</button>*/}
			{/*<button onClick={() => console.log('getValues()',setValue('masterKey', 'test'))}>InsideSet</button>*/}
			<form className={className} onSubmit={handleSubmit(onSubmit)}>
				{children}
			</form>
		</FormProvider>
	);
}

export default React.forwardRef(BaseForm);
