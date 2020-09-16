import React, { useState, useEffect, useRef } from 'react'
import { useSpring, animated } from 'react-spring'
import ResizeObserver from 'resize-observer-polyfill'
import axios from 'axios'
import { protocol, host } from '../../env'
import Project from '../../model/Project'
import Node from '../../model/Node'
import Subnode from '../../model/Subnode'
import Mark from '../../model/Mark'
import Dropdown from './Dropdown'
import './MarkData.css'

type MarkDataProps = {
	isCreateModeInitially: boolean
}

const MarkData = ({ isCreateModeInitially }: MarkDataProps) => {

    // interface ISelectedObject {
    //     recentMark: string
    //     project: string
    //     node: string
    //     subnode: string
    //     mark: string
    // }
    // interface IOptionsObject {
    //     recentMark: string[]
    //     project: string[]
    //     node: string[]
    //     subnode: string[]
    //     mark: string[]
    // }

    // Max lengths of input fields strings
    const seriesStringLength = 30
    const nodeStringLength = 10
    const subnodeStringLength = 10
    const markStringLength = 40
    const fullNameStringLength = 90

    // Select and Create modes
    const [isCreateMode, setIsCreateMode] = useState(isCreateModeInitially)

    const defaultSelectedObject = {
        recentMark: '',
        project: null as Project,
        node: null as Node,
        subnode: null as Subnode,
        mark: null as Mark
    }
    const [selectedObject, setSelectedObject] = useState(defaultSelectedObject)

    const defaultOptionsObject = {
        recentMarks: [] as string[],
        projects: [] as Project[],
        nodes: [] as Node[],
        subnodes: [] as Subnode[],
        marks: [] as Mark[]
    }
    const [optionsObject, setOptionsObject] = useState(defaultOptionsObject)
    
	// const [mark, setMark] = useState<Mark>(new Mark(null))
	// const [projects, setProjects] = useState<Array<string>>([])
	// const [nodes, setNodes] = useState<Array<string>>([])
	// const [subnodes, setSubnodes] = useState<Array<string>>([])
    // const [marks, setMarks] = useState<Array<string>>([])
    
    // const [markFullName, setMarkFullName] = useState('')
    // const [latestMarks, setLatestMarks] = useState<string[]>([])

    const [dropdownHeight, setDropdownHeight] = useState(0)
    // TBD
    const [infoHeight, setInfoHeight] = useState(0)
    const height1Ref = useRef()
    const height2Ref = useRef()

	useEffect(() => {
        // const projectsFetched: Array<string> = ['M32788', 'V32788', 'G32788']

        const fetchData = async () => {
            try {
                const projectsFetchedResponse = await axios.get(protocol + '://' + host + '/api/projects')
                const projectsFetched = projectsFetchedResponse.data

                console.log(projectsFetchedResponse)
                console.log(projectsFetchedResponse.data)
                const recentMarksFetched: Array<string> = [
                    'M32788.111.111-KVB 8',
                    'V62788.121.01-ZB11',
                    'V62GHV.121.01-ZB11',
                    'V62VB121.01-ZB11',
                    'V62FD8.121.01-ZB11',
                    'V6278V.21.01-ZB11',
                    'V62788.121.01-ZB11',
                    'D62SDS788.121.01-ZB11',
                    'DSS.121.01-ZB11',
                    'D6C88.121.01-ZB11',
                    'D62SFDDS788.121.01-ZB11',
                    'D62CVS788.121.01-ZB11',
                    'D62CCSDS788.121.01-ZB11'
                ]
                setOptionsObject({
                    ...defaultOptionsObject,
                    recentMarks: recentMarksFetched,
                    projects: projectsFetched
                })
            } catch (e) {
                console.log('Failed to fetch the data')
            }
        }
       
        fetchData()
        
        const ro1 = new ResizeObserver(([entry]) => {
			setDropdownHeight(entry.target.scrollHeight)
		})

		if (height1Ref.current) {
			ro1.observe(height1Ref.current)
        }
        
        const ro2 = new ResizeObserver(([entry]) => {
			setInfoHeight(entry.target.scrollHeight)
		})

		if (height2Ref.current) {
			ro2.observe(height1Ref.current)
		}

		return () => {
            ro1.disconnect()
            ro2.disconnect()
        }
    }, [height1Ref, height2Ref])


    const recentMarksSpringProp = useSpring({
		from: { opacity: 0 as any, height: 0 },
		to: {
            opacity: isCreateMode ? (0 as any) : 1,
            height: isCreateMode ? 0 : dropdownHeight,
        },
    })

    const nodeSpringProp = useSpring({
		from: { opacity: 0 as any, height: 0 },
		to: {
            opacity: selectedObject.project !== null ? 1 : (0 as any),
            height: selectedObject.project !== null ? dropdownHeight : 0,
        },
    })
    
    const subnodeSpringProp = useSpring({
		from: { opacity: 0 as any, height: 0 },
		to: {
            opacity: selectedObject.node !== null ? 1 : (0 as any),
            height: selectedObject.node !== null ? dropdownHeight : 0,
        },
    })
    
    const markSpringProp = useSpring({
		from: { opacity: 0 as any, height: 0 },
		to: {
            opacity: (selectedObject.subnode !== null && !isCreateMode) ? 1 : (0 as any),
            height: (selectedObject.subnode !== null && !isCreateMode) ? dropdownHeight : 0,
        },
    })
    
    const textSpringProp = useSpring({
		from: { opacity: 0 as any, height: 0 },
		to: {
            opacity: isCreateMode ? (0 as any) : 1,
            height: isCreateMode ? 0 : 25.6,
        },
    })
    
    const onRecentMarkSelect = (id: number) => {
        // TBD

        // const v = optionsObject.recentMarks[id]

        // const fetchedNodes: Array<string> = ['527', '127', '134']
        // const fetchedSubnodes: Array<string> = ['11', '22', '33']
        // const fetchedMarks: Array<string> = ['AVS 1', 'RTY 6', 'ZXE111']

        // setOptionsObject({
        //     ...defaultOptionsObject,
        //     recentMarks: optionsObject.recentMarks,
        //     projects: optionsObject.projects,
        //     nodes: fetchedNodes,
        //     subnodes: fetchedSubnodes,
        //     marks: fetchedMarks
        // })
        // setSelectedObject({
        //     ...defaultSelectedObject,
        //     recentMark: v,
        //     project: 'M32788',
        //     node: '127',
        //     subnode: '33',
        //     mark: 'AVS 1'
        // })
    }

    const onProjectSelect = async (id: number) => {
        const v = optionsObject.projects[id]
        if (v === selectedObject.project) {
            return
        }
        
        try {
            const fetchedNodesResponse = await axios.get(protocol + '://' + host + '/api/projects/{id}/nodes')
            const fetchedNodes = fetchedNodesResponse.data

            setOptionsObject({
                ...defaultOptionsObject,
                recentMarks: optionsObject.recentMarks,
                projects: optionsObject.projects,
                nodes: fetchedNodes
            })
            setSelectedObject({
                ...defaultSelectedObject,
                project: v
            })
        } catch (e) {
            console.log('Failed to fetch the data')
        }
	}

    const onNodeSelect = async (id: number) => {
        const v = optionsObject.nodes[id]
        if (v === selectedObject.node) {
            return
        }

        try {
            const fetchedSubnodesResponse = await axios.get(protocol + '://' + host + '/api/nodes/{id}/subnodes')
            const fetchedSubnodes = fetchedSubnodesResponse.data

            setOptionsObject({
                ...defaultOptionsObject,
                recentMarks: optionsObject.recentMarks,
                projects: optionsObject.projects,
                nodes: optionsObject.nodes,
                subnodes: fetchedSubnodes
            })
            setSelectedObject({
                ...defaultSelectedObject,
                project: selectedObject.project,
                node: v
            })
        } catch (e) {
            console.log('Failed to fetch the data')
        }

		// let gipSurname = ''
		// if (v !== '') {
		// 	// Fetch
		// 	const subnodes: Array<string> = ['527', '127', '134']
		// 	gipSurname = 'Влад'

		// 	setSubnodes(subnodes)
		// } else {
		// 	setSubnodes([])
		// }
		// setMarks([])
		// setMark({
		// 	...new Mark(null),
		// 	project: mark.project,
		// 	node: v,
		// 	gipSurname: gipSurname,
		// })
	}

    const onSubnodeSelect = async (id: number) => {
        const v = optionsObject.subnodes[id]
        if (v === selectedObject.subnode) {
            return
        }

        try {
            const fetchedMarksResponse = await axios.get(protocol + '://' + host + '/api/subnodes/{id}/marks')
            const fetchedMarks = fetchedMarksResponse.data

            setOptionsObject({
                ...defaultOptionsObject,
                recentMarks: optionsObject.recentMarks,
                projects: optionsObject.projects,
                nodes: optionsObject.nodes,
                subnodes: optionsObject.subnodes,
                marks: fetchedMarks
            })
            setSelectedObject({
                ...defaultSelectedObject,
                project: selectedObject.project,
                node: selectedObject.node,
                subnode: v
            })
        } catch (e) {
            console.log('Failed to fetch the data')
        }

		// let facilityName = ''
		// let objectName = ''
		// if (v !== '') {
		// 	// Fetch
		// 	const codes: Array<string> = ['AVS 1', 'RTY 6', 'ZXE111']
		// 	facilityName =
		// 		'Lorem Ipsum - это текст-"рыба", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной "рыбой" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн.'
		// 	objectName =
		// 		'Lorem Ipsum - это текст-"рыба", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной "рыбой" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. Lorem Ipsum не только успешно пережил без заметных изменений пять веков, но и перешагнул в электронный дизайн.'

		// 	setMarks(codes)
		// } else {
		// 	setMarks([])
		// }
		// setMark({
		// 	...mark,
		// 	subnode: v,
		// 	facilityName: facilityName,
		// 	objectName: objectName,
		// })
    }
    
    const onMarkSelect = (id: number) => {
        const v = optionsObject.marks[id]
        if (v === selectedObject.mark) {
            return
        }
        // const fetchedMarks: Array<string> = ['AVS 1', 'RTY 6', 'ZXE111']
        // setOptionsObject({
        //     ...defaultOptionsObject,
        //     nodes: optionsObject.nodes,
        //     subnodes: optionsObject.subnodes,
        //     marks: fetchedMarks
        // })
        setSelectedObject({
            ...defaultSelectedObject,
            project: selectedObject.project,
            node: selectedObject.node,
            subnode: selectedObject.subnode,
            mark: v
        })

		// setMark({
		// 	...mark,
		// 	mark: v,
		// })
	}

	return (
		<div className="mark-data-cnt">
			<h1 className="text-centered">Марки</h1>
			<div className="tabs">
				<input
					type="radio"
					name="tab-btn"
					id="tab-btn-1"
					value=""
					onChange={() => setIsCreateMode(false)}
					checked={isCreateMode ? false : true}
				/>
				<label htmlFor="tab-btn-1">Редактировать</label>
				<input
					type="radio"
					name="tab-btn"
					id="tab-btn-2"
					value=""
					onChange={() => setIsCreateMode(true)}
					checked={isCreateMode ? true : false}
				/>
				<label htmlFor="tab-btn-2">Добавить</label>

				<div className="flex-v">
					{/* {isCreateMode ? null : (
                        <InputArea label="Последние марки" widthClassName={'w-latest-marks'} onChangeFunc={onRecentMarkSelect} value={markFullName} options={latestMarks} />
					)} */}
                    {/* <div className="flex"> */}

                    <p className="text-centered section-label">{isCreateMode ? 'Выберите подузел' : 'Выберите марку'}</p>

                    <animated.div style={recentMarksSpringProp}>
						<div>
                    <Dropdown
                        label="Последние марки"
                        widthClassName={'1input-width-1'}
                        maxInputLength={fullNameStringLength}
                        onClickFunc={onRecentMarkSelect}
                        value={selectedObject.recentMark}
                        options={optionsObject.recentMarks}
                    />
                    </div>
					</animated.div>

                    <animated.div style={textSpringProp}>
                        <p ref={height2Ref} className="text-centered">или</p>
                    </animated.div>

                    <Dropdown
                            label="Базовая серия"
                            widthClassName={'1input-width-2'}
                            maxInputLength={seriesStringLength}
                            onClickFunc={onProjectSelect}
                            value={selectedObject.project}
                            options={optionsObject.projects}
                        />

                    <animated.div style={nodeSpringProp}>
						<div ref={height1Ref}>
                            <Dropdown
                                label="Узел"
                                widthClassName={'1input-width-3'}
                                maxInputLength={nodeStringLength}
                                onClickFunc={onNodeSelect}
                                value={selectedObject.node}
                                options={optionsObject.nodes}
                            />
						</div>
					</animated.div>

                    <animated.div style={subnodeSpringProp}>
						<div>
                            <Dropdown
                                label="Подузел"
                                widthClassName={'1input-width-3'}
                                maxInputLength={subnodeStringLength}
                                onClickFunc={onSubnodeSelect}
                                value={selectedObject.subnode}
                                options={optionsObject.subnodes}
                            />
						</div>
					</animated.div>

                    <animated.div style={markSpringProp}>
						<div>
                            <Dropdown
                                label="Марка"
                                widthClassName={'1input-width-3'}
                                maxInputLength={markStringLength}
                                onClickFunc={onMarkSelect}
                                value={selectedObject.mark}
                                options={optionsObject.marks}
                            />
						</div>
					</animated.div>

                    <p className="text-centered section-label">Данные марки</p>
                    
                    {/* <p className="text-centered">Информация</p>

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
                        {mark.mark === '' ? null : (
							<InputArea label="Отдел" widthClassName={'input-width-1'} onChangeFunc={onSubnodeSelect} value={mark.subnode} options={subnodes} />

						)}
                        {mark.gipSurname === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">Фамилия ГИПа</p>
								<p className="border input-border-radius input-padding">
									{mark.gipSurname}
								</p>
							</div>
						)} */}


                        {/* <Dropdown
                            label="Узел"
                            widthClassName={'1input-width-3'}
                            maxInputLength={nodeStringLength}
                            onClickFunc={onNodeSelect}
                            value={mark.node}
                            options={nodes}
                        />
                        <Dropdown
                            label="Подузел"
                            widthClassName={'1input-width-3'}
                            maxInputLength={subnodeStringLength}
                            onClickFunc={onSubnodeSelect}
                            value={mark.subnode}
                            options={subnodes}
                        /> */}

                    {/* <div className="flex-bot-v mrg-top mrg-bot">
                        
                    </div> */}
					
                    {/* <div className="flex-bot-v mrg-top mrg-bot">
                        <InputArea label="Базовая серия" widthClassName={'input-width-1'} onChangeFunc={onSeriesSelect} value={mark.series} options={series} />
						<p className="mrg-left mrg-right">.</p>
                        <InputArea label="Узел" widthClassName={'input-width-0'} onChangeFunc={onNodeSelect} value={mark.node} options={nodes} />
						<p className="mrg-left mrg-right">.</p>
                        <InputArea label="Подузел" widthClassName={'input-width-0'} onChangeFunc={onSubnodeSelect} value={mark.subnode} options={subnodes} />
						{isCreateMode ? null : (
							<p className="mrg-left mrg-right">-</p>
						)}
						{isCreateMode ? null : (
                            <InputArea label="Марка" widthClassName={'input-width-0'} onChangeFunc={onCodeSelect} value={mark.code} options={codes} />
						)}
					</div> */}
					{/* <div className="mrg-bot">
                        {mark.code === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">Обозначение марки</p>
								<p className="border input-border-radius input-padding">
									{mark.series+'.'+mark.node+'.'+mark.subnode+'-'+mark.code}
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
                        {mark.code === '' ? null : (
							<InputArea label="Отдел" widthClassName={'input-width-1'} onChangeFunc={onSubnodeSelect} value={mark.subnode} options={subnodes} />

						)}
                        {mark.gipSurname === '' ? null : (
							<div className="mrg-bot">
								<p className="mrg-bot-1">Фамилия ГИПа</p>
								<p className="border input-border-radius input-padding">
									{mark.gipSurname}
								</p>
							</div>
						)}
					</div> */}

					{isCreateMode ? (
						selectedObject.subnode === null ? null : (
							<button className="input-border-radius pointer">
								Добавить новую марку
							</button>
						)
					) : (
						selectedObject.mark === null ? null : <button className="input-border-radius pointer">
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
