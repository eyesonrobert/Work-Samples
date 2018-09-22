import * as React from 'react';
import { validateFields, formatTestCase, ITestCase } from "../../common/DynamicRuleValidation";
import { error, debug } from 'util';
import { Card } from '../../common/card';
import { AwardTypeApi } from './AwardTypeApi';
import AwardTypeList from './AwardTypeList';
import AwardTypeForm from './AwardTypeForm';

export interface IAwardTypeEntity {
    id: number,
    typeName: string,
    canDelete: boolean
}

export interface IAwardTypePage {
    awardTypeForm: IAwardTypeEntity,
    awardTypeList: IAwardTypeEntity[],
    isATUpdated: boolean,
    isFormValid: boolean,
    error: any
}

class AwardTypePage extends React.Component<{}, IAwardTypePage>{
    constructor(props) {
        super(props);
        this.state = {
            awardTypeForm: {
                id: null,
                typeName: '',
                canDelete: true
            },
            awardTypeList: [],
            isATUpdated: false,
            isFormValid: false,
            error: {
                typeName: '',
            }
        }
    }

    onGetAwardTypes = () => {
        AwardTypeApi.getAllAwardTypes()
            .then(resp => {
                this.setState({
                    awardTypeList: resp.items
                })
            })
            .catch(err => console.log(err));
    }

    componentDidMount() {
        this.onGetAwardTypes();
    }

    onChange = (fieldName, fieldValue) => {
        let nextState = {
            ...this.state,
            awardTypeForm: {
                ...this.state.awardTypeForm,
                [fieldName]: fieldValue
            }
        }
        this.setState(nextState, () => this.validateFields(this.state.awardTypeForm, fieldName));
    }

    validateFields = (form: any, fieldName: string) => {
        let tests: ITestCase[] = new Array<ITestCase>();
        for (let field in form) {
            let rules = {};
            switch (field) {
                case "typeName":
                    rules = {
                        minLength: 2,
                        maxLength: 50
                    }
                    break;
                default:
                    break;
            }
            tests.push(formatTestCase(form[field], field, rules, new Array<string>()))
        }
        tests = validateFields(tests);

        let newErrMsgs = { ...this.state.error };
        let currentFieldTest = tests.find(test => test.field == fieldName);
        if (currentFieldTest.errMsg.length > 0 && currentFieldTest.value)
            newErrMsgs = { ...this.state.error, [fieldName]: currentFieldTest.errMsg[0] };
        else newErrMsgs = { ...this.state.error, [fieldName]: "" }
        this.setState({ ...this.state, isFormValid: tests.every(test => test.errMsg.length == 0), error: newErrMsgs })
    }

    onSubmit = () => {
        AwardTypeApi.postAwardType(this.state.awardTypeForm)
            .then(resp => {
                this.setState({
                    awardTypeForm: {
                        id: null,
                        typeName: '',
                        canDelete: true
                    }
                });
                this.onGetAwardTypes();
            })
            .catch(err => console.log(err));
    }

    onEdit = id => {
        AwardTypeApi.getAwardTypeById(id)
            .then(resp => {
                let selected = resp.item;
                this.setState({
                    awardTypeForm: selected,
                    isATUpdated: true
                })
            })
            .catch(err => console.log(err));
    }

    onUpdate = () => {
        AwardTypeApi.updateAwardType(this.state.awardTypeForm)
            .then(resp => {
                this.setState({
                    awardTypeForm: {
                        id: null,
                        typeName: '',
                        canDelete: true
                    },
                    isATUpdated: false
                });
                this.onGetAwardTypes();
            })
            .catch(err => console.log(err));
    }

    onDelete = (id) => {
        AwardTypeApi.deleteAwardType(id)
            .then(resp => {
                this.onGetAwardTypes();
            })
    }

    render() {
        return (
            <div className="container col-md-12" >
                <div className="row">
                    <div className="col-md-6">
                        <div>
                            <AwardTypeForm
                                awardTypeForm={this.state.awardTypeForm}
                                onChange={this.onChange}
                                onClick={this.onSubmit}
                                onUpdate={this.onUpdate}
                                isATUpdated={this.state.isATUpdated}
                                disabled={!this.state.isFormValid}
                                error={this.state.error}
                            />
                        </div>
                    </div>
                    <div className="col-md-6">

                        <AwardTypeList
                            getAllAwardTypes={this.state.awardTypeList}
                            onEdit={this.onEdit}
                            onDelete={this.onDelete}
                        />
                    </div>
                </div>
            </div>
        );
    }
}
export default AwardTypePage;