// Global
import React, { useState, useEffect } from 'react'
import { useHistory } from 'react-router-dom'
import Select from 'react-select'
// Bootstrap
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
// Util
import httpClient from '../../axios'
import Employee from '../../model/Employee'
import ErrorMsg from '../ErrorMsg/ErrorMsg'
import Doc from '../../model/Doc'
import DocType from '../../model/DocType'
import { useMark } from '../../store/MarkStore'
import getFromOptions from '../../util/get-from-options'
import getNullableFieldValue from '../../util/get-field-value'
import { reactSelectStyle } from '../../util/react-select-style'

type DevelopingAttachedDocDataProps = {
	developingAttachedDoc: Doc
	isCreateMode: boolean
}

const DevelopingAttachedDocData = ({
	developingAttachedDoc,
	isCreateMode,
}: DevelopingAttachedDocDataProps) => {
	const defaultOptionsObject = {
		types: [] as DocType[],
		employees: [] as Employee[],
	}

	const history = useHistory()
	const mark = useMark()

	const [selectedObject, setSelectedObject] = useState<Doc>(
		isCreateMode
			? {
					id: -1,
					num: 1,
					numOfPages: 1,
					form: 1.0,
					name: '',
					type: null,
					creator: null,
					inspector: null,
					normContr: null,
					releaseNum: 0,
					note: '',
			  }
			: developingAttachedDoc
	)
	const [optionsObject, setOptionsObject] = useState(defaultOptionsObject)

	const [errMsg, setErrMsg] = useState('')

	useEffect(() => {
		if (mark != null && mark.id != null) {
			if (selectedObject == null) {
				history.push('/developing-attached-docs')
				return
			}
			const fetchData = async () => {
				try {
					const docTypesResponse = await httpClient.get(
						`/doc-types/attached`
					)
					const employeesResponse = await httpClient.get(
						`/departments/${mark.department.id}/employees`
					)
					setOptionsObject({
						employees: employeesResponse.data,
						types: docTypesResponse.data,
					})
				} catch (e) {
					console.log('Failed to fetch the data')
				}
			}
			fetchData()
		}
	}, [mark])

	const onCodeSelect = async (id: number) => {
		if (id == null) {
			setSelectedObject({
				...selectedObject,
				type: null,
			})
		}
		const v = getFromOptions(id, optionsObject.types, selectedObject.type)
		if (v != null) {
			setSelectedObject({
				...selectedObject,
				type: v,
			})
		}
	}

	const onNameChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
		setSelectedObject({
			...selectedObject,
			name: event.currentTarget.value,
		})
	}

	const onNumOfPagesChange = (event: React.FormEvent<HTMLInputElement>) => {
		setSelectedObject({
			...selectedObject,
			numOfPages: parseInt(event.currentTarget.value),
		})
	}

	const onFormatChange = (event: React.FormEvent<HTMLInputElement>) => {
		setSelectedObject({
			...selectedObject,
			form: parseFloat(event.currentTarget.value),
		})
	}

	const onNoteChange = (event: React.FormEvent<HTMLTextAreaElement>) => {
		setSelectedObject({
			...selectedObject,
			note: event.currentTarget.value,
		})
	}

	const onCreatorSelect = async (id: number) => {
		if (id == null) {
			setSelectedObject({
				...selectedObject,
				creator: null,
			})
		}
		const v = getFromOptions(
			id,
			optionsObject.employees,
			selectedObject.creator
		)
		if (v != null) {
			setSelectedObject({
				...selectedObject,
				creator: v,
			})
		}
	}

	const onInspectorSelect = async (id: number) => {
		if (id == null) {
			setSelectedObject({
				...selectedObject,
				inspector: null,
			})
		}
		const v = getFromOptions(
			id,
			optionsObject.employees,
			selectedObject.inspector
		)
		if (v != null) {
			setSelectedObject({
				...selectedObject,
				inspector: v,
			})
		}
	}

	const onNormControllerSelect = async (id: number) => {
		if (id == null) {
			setSelectedObject({
				...selectedObject,
				normContr: null,
			})
		}
		const v = getFromOptions(
			id,
			optionsObject.employees,
			selectedObject.normContr
		)
		if (v != null) {
			setSelectedObject({
				...selectedObject,
				normContr: v,
			})
		}
	}

	const checkIfValid = () => {
		if (selectedObject.type == null) {
			setErrMsg('Пожалуйста, выберите шифр прилагаемого документа')
			return false
		}
		if (selectedObject.name === '') {
			setErrMsg('Пожалуйста, введите наименование прилагаемого документа')
			return false
		}
		if (isNaN(selectedObject.form)) {
			setErrMsg('Пожалуйста, введите формат прилагаемого документа')
			return false
		}
		return true
	}

	const onCreateButtonClick = async () => {
		if (checkIfValid()) {
			try {
				await httpClient.post(`/marks/${mark.id}/docs`, {
					typeId: selectedObject.type.id,
					name: selectedObject.name,
					numOfPages: selectedObject.numOfPages,
					form: selectedObject.form,
					creatorId: selectedObject.creator?.id,
					inspectorId: selectedObject.inspector?.id,
					normContrId: selectedObject.normContr?.id,
					note: selectedObject.note,
				})
				history.push('/developing-attached-docs')
			} catch (e) {
				setErrMsg('Произошла ошибка')
				console.log('Error')
			}
		}
	}

	const onChangeButtonClick = async () => {
		if (checkIfValid()) {
			try {
				await httpClient.patch(`/docs/${selectedObject.id}`, {
					typeId:
						selectedObject.type.id === developingAttachedDoc.type.id
							? undefined
							: selectedObject.type.id,
					name:
						selectedObject.name === developingAttachedDoc.name
							? undefined
							: selectedObject.name,
					numOfPages:
						selectedObject.numOfPages ===
						developingAttachedDoc.numOfPages
							? undefined
							: selectedObject.numOfPages,
					form:
						selectedObject.form === developingAttachedDoc.form
							? undefined
							: selectedObject.form,
					creatorId: getNullableFieldValue(
						selectedObject.creator,
						developingAttachedDoc.creator
					),
					inspectorId: getNullableFieldValue(
						selectedObject.inspector,
						developingAttachedDoc.inspector
					),
					normContrId: getNullableFieldValue(
						selectedObject.normContr,
						developingAttachedDoc.normContr
					),
					note:
						selectedObject.note === developingAttachedDoc.note
							? undefined
							: selectedObject.note,
				})
				history.push('/developing-attached-docs')
			} catch (e) {
				setErrMsg('Произошла ошибка')
				console.log('Error')
			}
		}
	}

	return selectedObject == null || mark == null ? null : (
		<div className="component-cnt flex-v-cent-h">
			<h1 className="text-centered">
				{isCreateMode
					? 'Создание прилагаемого документа'
					: 'Данные прилагаемого документа'}
			</h1>
			<div className="shadow p-3 mb-5 bg-white rounded component-width component-cnt-div">
				<Form.Group className="flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="code"
						style={{ marginRight: '1em' }}
					>
						Шифр документа
					</Form.Label>
					<Select
						inputId="code"
						maxMenuHeight={250}
						isClearable={true}
						isSearchable={true}
						placeholder="Выбор шифр документа"
						noOptionsMessage={() => 'Шифры не найдены'}
						className="auto-width flex-grow"
						onChange={(selectedOption) =>
							onCodeSelect((selectedOption as any)?.value)
						}
						value={
							selectedObject.type == null
								? null
								: {
										value: selectedObject.type.id,
										label: selectedObject.type.code,
								  }
						}
						options={optionsObject.types.map((t) => {
							return {
								value: t.id,
								label: t.code,
							}
						})}
						styles={reactSelectStyle}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2">
					<Form.Label htmlFor="name">Наименование</Form.Label>
					<Form.Control
						id="name"
						as="textarea"
						rows={4}
						style={{ resize: 'none' }}
						placeholder="Введите наименование"
						value={selectedObject.name}
						onChange={onNameChange}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2 flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="numOfPages"
						style={{ marginRight: '2.85em' }}
					>
						Число листов
					</Form.Label>
					<Form.Control
						id="numOfPages"
						type="text"
						placeholder="Введите число листов"
						autoComplete="off"
						defaultValue={
							isNaN(selectedObject.numOfPages)
								? ''
								: selectedObject.numOfPages
						}
						className="auto-width flex-grow"
						onBlur={onNumOfPagesChange}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2 flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="format"
						style={{ marginRight: '5.6em' }}
					>
						Формат
					</Form.Label>
					<Form.Control
						id="format"
						type="text"
						placeholder="Введите формат"
						autoComplete="off"
						defaultValue={
							isNaN(selectedObject.form)
								? ''
								: selectedObject.form
						}
						onBlur={onFormatChange}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2 flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="creator"
						style={{ marginRight: '3.9em' }}
					>
						Разработал
					</Form.Label>
					<Select
						inputId="creator"
						maxMenuHeight={250}
						isClearable={true}
						isSearchable={true}
						placeholder="Выбор разработчика"
						noOptionsMessage={() => 'Сотрудники не найдены'}
						className="auto-width flex-grow"
						onChange={(selectedOption) =>
							onCreatorSelect((selectedOption as any)?.value)
						}
						value={
							selectedObject.creator == null
								? null
								: {
										value: selectedObject.creator.id,
										label: selectedObject.creator.name,
								  }
						}
						options={optionsObject.employees.map((e) => {
							return {
								value: e.id,
								label: e.name,
							}
						})}
						styles={reactSelectStyle}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2 flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="inspector"
						style={{ marginRight: '4.5em' }}
					>
						Проверил
					</Form.Label>
					<Select
						inputId="inspector"
						maxMenuHeight={250}
						isClearable={true}
						isSearchable={true}
						placeholder="Выбор проверщика"
						noOptionsMessage={() => 'Сотрудники не найдены'}
						className="auto-width flex-grow"
						onChange={(selectedOption) =>
							onInspectorSelect((selectedOption as any)?.value)
						}
						value={
							selectedObject.inspector == null
								? null
								: {
										value: selectedObject.inspector.id,
										label: selectedObject.inspector.name,
								  }
						}
						options={optionsObject.employees.map((e) => {
							return {
								value: e.id,
								label: e.name,
							}
						})}
						styles={reactSelectStyle}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2 flex-cent-v">
					<Form.Label
						className="no-bot-mrg"
						htmlFor="normContr"
						style={{ marginRight: '1em' }}
					>
						Нормоконтролер
					</Form.Label>
					<Select
						inputId="normContr"
						maxMenuHeight={250}
						isClearable={true}
						isSearchable={true}
						placeholder="Выбор нормоконтролера"
						noOptionsMessage={() => 'Сотрудники не найдены'}
						className="auto-width flex-grow"
						onChange={(selectedOption) =>
							onNormControllerSelect(
								(selectedOption as any)?.value
							)
						}
						value={
							selectedObject.normContr == null
								? null
								: {
										value: selectedObject.normContr.id,
										label: selectedObject.normContr.name,
								  }
						}
						options={optionsObject.employees.map((e) => {
							return {
								value: e.id,
								label: e.name,
							}
						})}
						styles={reactSelectStyle}
					/>
				</Form.Group>

				<Form.Group className="mrg-top-2" style={{ marginBottom: 0 }}>
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

				<ErrorMsg errMsg={errMsg} hide={() => setErrMsg('')} />

				<Button
					variant="secondary"
					className="btn-mrg-top-2 full-width"
					onClick={
						isCreateMode ? onCreateButtonClick : onChangeButtonClick
					}
				>
					{isCreateMode
						? 'Создать разрабатываемый прилагаемый документ'
						: 'Сохранить изменения'}
				</Button>
			</div>
		</div>
	)
}

export default DevelopingAttachedDocData
