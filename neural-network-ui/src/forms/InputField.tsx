import { FC } from 'react';
import {Field} from 'formik';
interface Props {
    fieldName: string,
    placeholder: string
}
const InputField:FC<Props> = ({fieldName, placeholder}) =>
{
    return (
        <>
            <label htmlFor={fieldName}><b>{fieldName}: </b></label>
            <Field id={fieldName} name={fieldName} placeholder={placeholder} />
        </>
    )
}
export default InputField;