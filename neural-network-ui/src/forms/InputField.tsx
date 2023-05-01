import { FC } from 'react';
import { Field } from 'formik';
import 'react-tooltip/dist/react-tooltip.css';
import {Tooltip as ReactTooltip} from "react-tooltip";

interface Props {
    fieldName: string,
    placeholder: string,
    error: string|undefined,
    touched: boolean|undefined
}
const InputField:FC<Props> = ({fieldName, placeholder, error, touched}) =>
{
    return (
        <>
        {touched && error ? (
                < >
                    <label htmlFor={fieldName}><b>{fieldName}: </b></label>
                    <Field className="error" data-tooltip-id={`${fieldName}Id`} data-tooltip-content={error} id={fieldName} name={fieldName} placeholder={placeholder} />
                    <ReactTooltip  id={`${fieldName}Id`} place="top"/>
                </>
            ): <>
                    <label htmlFor={fieldName}><b>{fieldName}: </b></label>
                    <Field id={fieldName} name={fieldName} placeholder={placeholder} />
            </>}
        </>
    )
}
export default InputField;