// Global
import React, { useState, useEffect } from 'react'
import { useHistory } from 'react-router-dom'
// Bootstrap
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
// Util
import httpClient from '../../axios'
import Specification from '../../model/Specification'
import Construction from '../../model/Construction'
import StandardConstruction from '../../model/StandardConstruction'
import { useMark } from '../../store/MarkStore'
import ConstructionTable from '../Construction/ConstructionTable'
import StandardConstructionTable from '../StandardConstruction/StandardConstructionTable'

type SpecificationDataProps = {
	specification: Specification
	setConstruction: (c: Construction) => void
	copiedConstruction: Construction
	setCopiedConstruction: (c: Construction) => void
	setStandardConstruction: (sc: StandardConstruction) => void
}

const SpecificationData = ({
	specification,
	setConstruction,
	copiedConstruction,
	setCopiedConstruction,
	setStandardConstruction,
}: SpecificationDataProps) => {
	const history = useHistory()
	const mark = useMark()

	const [selectedObject, setSelectedObject] = useState(specification)

	useEffect(() => {
		if (mark != null && mark.id != null && selectedObject == null) {
			history.push('/specifications')
		}
	}, [mark])

	const onNoteChange = async (
		event: React.FormEvent<HTMLTextAreaElement>
	) => {
		setSelectedObject({
			...selectedObject,
			note: event.currentTarget.value,
		})
	}

	const onChangeButtonClick = async () => {
		try {
			await httpClient.patch(`/specifications/${selectedObject.id}`, {
				note: selectedObject.note,
			})
			history.push('/specifications')
		} catch (e) {
			console.log('Error')
		}
	}

	return selectedObject == null || mark == null ? null : (
		<div className="component-cnt flex-v-cent-h">
			<h1 className="text-centered">Данные выпуска спецификации</h1>
			<div className="shadow p-3 mb-5 bg-white rounded component-width-2 component-cnt-div">
				<div className="space-between">
					<Form.Group className="flex-cent-v">
						<Form.Label
							className="no-bot-mrg"
							style={{ marginRight: '.75em' }}
						>
							Номер
						</Form.Label>
						<Form.Control
							type="text"
							value={selectedObject.num}
							readOnly={true}
							className="text-centered"
							style={{ width: '50px' }}
						/>
					</Form.Group>
					<Form.Group>
						<Form.Check
							// ref={refs[index]}
							// id={`is${s.id}`}
							name="currentRelease"
							type="radio"
							style={{ pointerEvents: 'none' }}
							checked={selectedObject.isCurrent}
							readOnly={true}
						/>
					</Form.Group>
				</div>
				<Form.Group className="no-bot-mrg">
					<Form.Label htmlFor="note">Примечание</Form.Label>
					<Form.Control
						id="note"
						as="textarea"
						rows={4}
						style={{ resize: 'none' }}
						placeholder="Не введено"
						defaultValue={selectedObject.note}
						onBlur={onNoteChange}
					/>
				</Form.Group>
				<Button
					variant="secondary"
					className="btn-mrg-top-2 full-width"
					onClick={onChangeButtonClick}
				>
					Сохранить изменения
				</Button>
			</div>

			<ConstructionTable
				setConstruction={setConstruction}
				copiedConstruction={copiedConstruction}
				setCopiedConstruction={setCopiedConstruction}
				specificationId={selectedObject.id}
			/>
			<StandardConstructionTable
				setStandardConstruction={setStandardConstruction}
				specificationId={selectedObject.id}
			/>
		</div>
	)
}

export default SpecificationData
