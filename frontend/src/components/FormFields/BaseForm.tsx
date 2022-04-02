import React, { createElement, ReactElement } from "react";
import { DeepPartial, SubmitHandler, useForm, FormProvider } from "react-hook-form";
import { DefaultValues } from "react-hook-form/dist/types/form";
import {Resolver} from "react-hook-form/dist/types/resolvers";

interface IFormProps<T> {
	onSubmit: SubmitHandler<T>;
	defaultValues?: DefaultValues<T>;
	className?: string;
	resolver?: Resolver<T>;
}

export function BaseForm<T>(props: IFormProps<T> & { children: ReactElement | ReactElement[] }) {
	const { defaultValues, children, onSubmit, className, resolver } = props;
	const methods  = useForm<T>({ defaultValues, resolver });
	const { handleSubmit } = methods
	return (
		<FormProvider {...methods}>
			<form className={className} onSubmit={handleSubmit(onSubmit)}>
				{children}
			</form>
		</FormProvider>
	);
}
