import React, { useState, useEffect } from 'react'
import Mark from '../../model/Mark'
import './MarkData.css'

type MarkDataProps = {
	isCreateModeInitially: boolean
}

// !!!TBD set node to empty string when changeng series

const MarkData = ({ isCreateModeInitially }: MarkDataProps) => {
	const [isCreateMode, setIsCreateMode] = useState<boolean>(
		isCreateModeInitially
	)
	const [mark, setMark] = useState<Mark>(new Mark(null))

	const [series, setSeries] = useState<Array<string>>([])
	// const [selectedSeries, setSelectedSeries] = useState<string>("")

	const [nodes, setNodes] = useState<Array<string>>([])
	// const [selectedNode, setSelectedNode] = useState<string>("")

	const [subnodes, setSubnodes] = useState<Array<string>>([])
	// const [selectedSubnode, setSelectedSubnode] = useState<string>("")

	const [codes, setCodes] = useState<Array<string>>([])
	// const [selectedCode, setSelectedCode] = useState<string>("")

	useEffect(() => {
		const seriesFetched: Array<string> = ['M32788', 'V32788', 'G32788']
		setSeries(seriesFetched)
	}, [])

	const onSeriesSelect = (event: React.FormEvent<HTMLSelectElement>) => {
		const v = event.currentTarget.value
		if (v !== '') {
			// Fetch
			const nodes: Array<string> = ['527', '127', '134']

			setNodes(nodes)
		} else {
			setNodes([])
		}
		setSubnodes([])
		setCodes([])
		setMark({ ...new Mark(null), series: v })
	}

	const onNodeSelect = (event: React.FormEvent<HTMLSelectElement>) => {
		const v = event.currentTarget.value

		let gipSurname = ''
		if (v !== '') {
			// Fetch
			const subnodes: Array<string> = ['527', '127', '134']
			gipSurname = 'Влад'

			setSubnodes(subnodes)
		} else {
			setSubnodes([])
		}
		setCodes([])
		setMark({
			...new Mark(null),
			series: mark.series,
			node: v,
			gipSurname: gipSurname,
		})
	}

	const onSubnodeSelect = (event: React.FormEvent<HTMLSelectElement>) => {
		const v = event.currentTarget.value

		let facilityName = ''
		let objectName = ''
		if (v !== '') {
			// Fetch
			const codes: Array<string> = ['AVS 1', 'RTY 6', 'ZXE111']
			facilityName =
				'Lorem Ipsum - это текст-"рыба", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной "рыбой" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн.'
			objectName =
				'Lorem Ipsum - это текст-"рыба", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной "рыбой" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн.'

			setCodes(codes)
		} else {
			setCodes([])
		}
		setMark({
			...mark,
			subnode: v,
			facilityName: facilityName,
			objectName: objectName,
		})
    }
    
    const onCodeSelect = (event: React.FormEvent<HTMLSelectElement>) => {
        const v = event.currentTarget.value

		setMark({
			...mark,
			code: v,
		})
	}

	return (
		<div className="Mark-data-cnt">
			<h1 className="text-centered Mark-data-title">Данные по марке</h1>
			<div className="tabs">
				<input
					type="radio"
					name="tab-btn"
					id="tab-btn-1"
					value=""
					onChange={() => setIsCreateMode(false)}
					checked={isCreateMode ? false : true}
				/>
				<label htmlFor="tab-btn-1">Выбрать</label>
				<input
					type="radio"
					name="tab-btn"
					id="tab-btn-2"
					value=""
					onChange={() => setIsCreateMode(true)}
					checked={isCreateMode ? true : false}
				/>
				<label htmlFor="tab-btn-2">Добавить</label>

				<div className="Mark-data">
					{isCreateMode ? null : (
						<div className="flex-v mrg-bot">
							<p>Последние марки</p>
							<select className="w-latest-Marks mrg-right border input-border-radius input-padding">
								<option value=""></option>
								<option>M32788.111.111-KVB 8</option>
								<option>D32788.111.111-KVB 8</option>
							</select>
						</div>
					)}
					<div className="flex-bot-v mrg-bot">
						<div className="flex-v">
							<p className="mrg-bot-1">Базовая серия</p>
							<select
                                onChange={onSeriesSelect}
                                value={mark.series}
								className="input-width-1 border input-border-radius input-padding"
							>
								<option key={-1}></option>
								{series.map((x, y) => (
									<option key={y}>{x}</option>
								))}
							</select>
						</div>

						<p className="mrg-left mrg-right">.</p>

						<div className="flex-v">
							<p className="mrg-bot-1">Узел</p>
							<select
                                onChange={onNodeSelect}
                                value={mark.node}
								className="input-width-0 border input-border-radius input-padding"
							>
								<option key={-1}></option>
								{nodes.map((x, y) => (
									<option key={y}>{x}</option>
								))}
							</select>
						</div>

						<p className="mrg-left mrg-right">.</p>

						<div className="flex-v">
							<p className="mrg-bot-1">Подузел</p>
							<select
                                onChange={onSubnodeSelect}
                                value={mark.subnode}
								className="input-width-0 border input-border-radius input-padding"
							>
								<option key={-1}></option>
								{subnodes.map((x, y) => (
									<option key={y}>{x}</option>
								))}
							</select>
						</div>
						{isCreateMode ? null : (
							<p className="mrg-left mrg-right">-</p>
						)}
						{isCreateMode ? null : (
							<div className="flex-v">
								<p className="mrg-bot-1">Марка</p>
								<select onChange={onCodeSelect} value={mark.code} className="input-width-0 border input-border-radius input-padding">
									<option key={-1}></option>
									{codes.map((x, y) => (
										<option key={y}>{x}</option>
									))}
									{/* <option value="" hidden>Марка</option>
                        <option>Пункт 1</option>
                        <option>Пункт 2</option> */}
								</select>
							</div>
						)}
					</div>
					<div className="mrg-bot">
						{mark.gipSurname === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">Фамилия ГИПа</p>
								<p className="border input-border-radius input-padding">
									{mark.gipSurname}
								</p>
							</div>
						)}
						{mark.facilityName === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">
									Наименование комплекса
								</p>
								<p className="border input-border-radius input-padding">
									{mark.facilityName}
								</p>
							</div>
						)}
						{mark.objectName === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">
									Наименование объекта
								</p>
								<p className="border input-border-radius input-padding">
									{mark.objectName}
								</p>
							</div>
						)}
					</div>

					{isCreateMode ? (
						mark.subnode === '' ? null : (
							<button className="input-border-radius pointer">
								Добавить новую марку
							</button>
						)
					) : (
						mark.code === '' ? null : <button className="input-border-radius pointer">
							Сохранить изменения
						</button>
					)}

					{/* <div className="space-between-cent-v mrg-bot">
                    <div className="flex">
                        <p>Отдел</p>
                        <select className="input-width-2 mrg-left mrg-right border input-border-radius input-padding">
                            <option>Пункт 1</option>
                            <option>Пункт 2</option>
                        </select>
                    </div>
                    <div className="flex">
                        <p>Шифр марки</p>
                        <select className="input-width-1 mrg-left mrg-right border input-border-radius input-padding">
                            <option>Пункт 1</option>
                            <option>Пункт 2</option>
                        </select>
                    </div>
                </div>
                <div className="mrg-bot">
                    Таблица
                </div>
                <div className="mrg-bot flex-cent-v">
                    <label htmlFor="agreements">Согласования</label>
                    <input className="mrg-left mrg-right checkbox" type="checkbox" id="agreements" name="agreements" />
                </div>
                <button className="input-border-radius pointer">Добавить новую марку</button> */}
				</div>
			</div>
		</div>
	)
}

export default MarkData