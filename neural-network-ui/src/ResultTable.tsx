import { Table, Menu, Icon } from 'semantic-ui-react'
import { useAppDispatch, useAppSelector } from './app/hooks'
import { NNTypes, setResult } from './features/neuralNetworkSlice'
import { useEffect } from 'react'
import { neuralNetworkService } from "./services/neuralNetworkService";

const ResultsTable = () => {
  const dispatch = useAppDispatch();
  const results = useAppSelector((state) => state.selectedNNType.results);
  useEffect(() => {
    neuralNetworkService.List().then(data =>
      dispatch(setResult(data != undefined ? data : [])))
  }, [results.length]);
  
  return (
    <Table celled>
      <Table.Header>
        <Table.Row>
          <Table.HeaderCell>Age</Table.HeaderCell>
          <Table.HeaderCell>Sex</Table.HeaderCell>
          <Table.HeaderCell>Cp</Table.HeaderCell>
          <Table.HeaderCell>Trestbps</Table.HeaderCell>
          <Table.HeaderCell>Chol</Table.HeaderCell>
          <Table.HeaderCell>Fbs</Table.HeaderCell>
          <Table.HeaderCell>Restecg</Table.HeaderCell>
          <Table.HeaderCell>Thalach</Table.HeaderCell>
          <Table.HeaderCell>Exang</Table.HeaderCell>
          <Table.HeaderCell>Oldpeak</Table.HeaderCell>
          <Table.HeaderCell>Slope</Table.HeaderCell>
          <Table.HeaderCell>Ca</Table.HeaderCell>
          <Table.HeaderCell>Thal</Table.HeaderCell>
          <Table.HeaderCell>Result</Table.HeaderCell>
          <Table.HeaderCell>NNType</Table.HeaderCell>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {results && results.length > 0 && results.map(x => (
          <Table.Row key={x.symptomId}>
            <Table.Cell>{x.age}</Table.Cell>
            <Table.Cell>{x.sex}</Table.Cell>
            <Table.Cell>{x.cp}</Table.Cell>
            <Table.Cell>{x.trestbps}</Table.Cell>
            <Table.Cell>{x.chol}</Table.Cell>
            <Table.Cell>{x.fbs}</Table.Cell>
            <Table.Cell>{x.restecg}</Table.Cell>
            <Table.Cell>{x.thalach}</Table.Cell>
            <Table.Cell>{x.exang}</Table.Cell>
            <Table.Cell>{x.oldpeak}</Table.Cell>
            <Table.Cell>{x.slope}</Table.Cell>
            <Table.Cell>{x.ca}</Table.Cell>
            <Table.Cell>{x.thal}</Table.Cell>
            <Table.Cell>{x.result > 0.5 ? "Helthy" : "Consult a doctor"}</Table.Cell>
            <Table.Cell>{x.nnType == NNTypes.FFNN ? "Feed Forward" : "Recurrent"}</Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
      <Table.Footer>
        <Table.Row>
          <Table.HeaderCell colSpan='15'>
            The maximum visible number of visiable predictions in list is 5.
          </Table.HeaderCell>
        </Table.Row>
      </Table.Footer>
    </Table>
  )
}
export default ResultsTable;

interface TableProps {
  value: string
}
