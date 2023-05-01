import { Formik, Form, FormikHelpers } from 'formik';
import { Header, Button } from 'semantic-ui-react'
import { useAppDispatch, useAppSelector} from '../app/hooks'
import './Form.css';
import { Symptomes, setResult } from "../features/neuralNetworkSlice";
import InputField from "./InputField";
import { neuralNetworkService } from "../services/neuralNetworkService";
import * as Yup from 'yup';

const initialValues: Symptomes = {
    Age: "",
    Sex: "",
    Trestbps: "",
    Chol: "",
    Fbs: "",
    Restecg: "",
    Thalach: "",
    Exang: "",
    Oldpeak: "",
    Slope: "",
    Ca: "",
    Thal: ""
}
const SymptomesSchemaValidation = Yup.object().shape({
    Age: Yup.number().moreThan(0, "Must be more then 0"),
    Sex: Yup.number().moreThan(-1).lessThan(2, "O is woman, 1 is man"),
    Trestbps: Yup.number().moreThan(0, "Must be more then 0"),
    Chol: Yup.number().moreThan(0, "Must be more then 0"),
    Fbs: Yup.number().moreThan(0, "Must be more then 0"),
    Restecg: Yup.number().moreThan(0, "Must be more then 0"),
    Thalach: Yup.number().moreThan(0, "Must be more then 0"),
    Exang: Yup.number().moreThan(0, "Must be more then 0"),
    Oldpeak: Yup.number().moreThan(0, "Must be more then 0"),
    Slope: Yup.number().moreThan(0, "Must be more then 0"),
    Ca: Yup.number().moreThan(0, "Must be more then 0"),
    Thal: Yup.number().moreThan(0, "Must be more then 0")
})
const SymptomesForm = () => {
    const dispatch = useAppDispatch();
    const selectedNN = useAppSelector((state) => state.selectedNNType.nnType);

    return(
        <Formik
            initialValues={initialValues}
            validationSchema={SymptomesSchemaValidation}
            onSubmit={(value: Symptomes, {setSubmitting}:FormikHelpers<Symptomes>) => {
                console.log(value)
                neuralNetworkService.Predict(value, selectedNN).then(data => {
                    dispatch(setResult(data ?? null))
                })
                setSubmitting(false)
            }
        }>
            {({ handleSubmit, isSubmitting, errors, touched }) => (
                <Form
                    onSubmit={handleSubmit}
                    autoComplete="off"
                    className="form"
                >
                    <Header
                        as="h2"
                        icon ="hire a helper"
                        textAlign="center"
                        content="Symptomes form"
                        className="formHeader"
                    />
                    <div className="userAndPassword">
                        <InputField error={errors.Age} touched={touched.Age} fieldName="Age" placeholder="Type your age"/>
                        <InputField error={errors.Sex} touched={touched.Sex} fieldName="Sex" placeholder="Type your sex"/>
                        <InputField error={errors.Trestbps} touched={touched.Trestbps} fieldName="Trestbps" placeholder="Type your Trestbps"/>
                        <InputField error={errors.Chol} touched={touched.Chol} fieldName="Chol" placeholder="Type your Chol"/>
                        <InputField error={errors.Fbs} touched={touched.Fbs} fieldName="Fbs" placeholder="Type your Fbs"/>
                        <InputField error={errors.Restecg} touched={touched.Restecg} fieldName="Restecg" placeholder="Type your Restecg"/>
                        <InputField error={errors.Thalach} touched={touched.Thalach} fieldName="Thalach" placeholder="Type your Thalach"/>
                        <InputField error={errors.Exang} touched={touched.Exang} fieldName="Exang" placeholder="Type your Exang"/>
                        <InputField error={errors.Oldpeak} touched={touched.Oldpeak} fieldName="Oldpeak" placeholder="Type your Oldpeak"/>
                        <InputField error={errors.Slope} touched={touched.Slope} fieldName="Slope" placeholder="Type your Slope"/>
                        <InputField error={errors.Ca} touched={touched.Ca} fieldName="Ca" placeholder="Type your Ca"/>
                        <InputField error={errors.Thal} touched={touched.Thal} fieldName="Thal" placeholder="Type your Thal"/>
                    </div>
                    <Button
                        loading={isSubmitting}
                        className="loginButton"
                        content="Predict"
                        type="submit"
                        fluid
                    />
            </Form>
            )}
        </Formik>
    )
}
export default SymptomesForm;