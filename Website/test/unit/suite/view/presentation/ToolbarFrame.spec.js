import expect from 'unittest/helpers/expect.js';
import sinon from 'sinon';
import React from 'react';
import TestUtils from 'react-addons-test-utils';
import ToolbarFrame from 'console/components/presentation/ToolbarFrame.js';
import Toolbar from 'console/components/presentation/Toolbar.js';
import TestComponent from 'test/unit/helpers/TestComponent.js';
import Immutable from 'immutable';

describe('ToolbarFrame', () => {
	let renderer, props, pageActions;
	beforeEach(() => {
		pageActions = {
			fireAction: () => {},
			update: () => {},
			setOption: () => {},
			save: () => {}
		};
		props = {
			pageName: 'test',
			toolbars: Immutable.fromJS([
				{
					name: 'test/toolbar',
					items: [
						{ name: 'test/onebutton', type: 'button', label: 'One', action: 'oneaction' },
						{ name: 'test/twobutton', type: 'button', label: 'Two', action: 'twoaction' },
						{ name: 'test/save', type: 'button', label: 'Save', action: 'save' }
					]
				}
			]),
			dirty: false,
			actions: {
				save: sinon.spy(() => pageActions.save).named('save'),
				fireAction: sinon.spy(() => pageActions.fireAction).named('fireAction'),
				setOption: sinon.spy(() => pageActions.setOption).named('setOption')
			},
			children: [<TestComponent/>]
		};
		renderer = TestUtils.createRenderer();
	});

	it('renders a toolbar and a single contained tab', () => {
		renderer.render(<ToolbarFrame name='test' {...props}/>);
		return expect(
			renderer,
			'to have rendered',
			<div className='page'>
				<Toolbar
					name='test/toolbar'
					items={[{}, {}, {}]}/>
				<TestComponent/>
			</div>
		)
		.and(
			'queried for', <Toolbar/>,
			'to have props exhaustively satisfying', {
				name: 'test/toolbar',
				items: [
					{ name: 'test/onebutton', type: 'button', label: 'One', action: pageActions.fireAction },
					{ name: 'test/twobutton', type: 'button', label: 'Two', action: pageActions.fireAction },
					{ name: 'test/save', type: 'button', label: 'Save', action: pageActions.save, disabled: true }
				]
			}
		);
	});

	it('passes named actions to the toolbar', () => {
		renderer.render(<ToolbarFrame name='test' {...props}/>);
		Promise.all([
			expect(renderer,
				'queried for', <Toolbar/>,
			'to have props satisfying', {
				name: 'test/toolbar',
				items: [
					{ action: expect.it('to be', pageActions.fireAction) },
					{ action: expect.it('to be', pageActions.fireAction) },
					{ action: expect.it('to be', pageActions.save) }
				]
			}),
			expect(props.actions.save, 'to have a call satisfying', { args: ['test'] }),
			expect(props.actions.fireAction, 'to have calls exhaustively satisfying', [
				{ args: ['oneaction', 'test'] },
				{ args: ['twoaction', 'test'] },
			])
		]);
	});

	it('passes useful props to selects, checkbox groups on toolbars', () => {
		props.toolbars = props.toolbars.setIn([0, 'items'], Immutable.fromJS([
			{ name: 'test/select', type: 'select', options: [{ value: 'opt1' }], value: 'opt1' },
			{ name: 'test/checks', type: 'checkboxGroup', options: [{ name: 'test/checks/c1', label: 'Check 1'}, { name: 'test/checks/c2', label: 'Check 2'}], value: ['test/checks/c1'] }
		]));
		renderer.render(<ToolbarFrame name='test' {...props}/>);
		Promise.all([
			expect(renderer,
				'queried for', <Toolbar/>,
			'to have props satisfying', {
				name: 'test/toolbar',
				items: [
					{ onChange: expect.it('to be a function'), options: [{ value: 'opt1' }], value: 'opt1' },
					{ onChange: expect.it('to be a function'), options: [{ name: 'test/checks/c1', label: 'Check 1' }, { name: 'test/checks/c2', label: 'Check 2' }], value: ['test/checks/c1'] }
				]
			}),
			expect(props.actions.setOption, 'to have a call satisfying', { args: ['test/select'] }),
			expect(props.actions.setOption, 'to have a call satisfying', { args: ['test/checks'] })
		]);
	});

	it('handles missing toolbar defs', () => {
		delete props.toolbars[0];
		renderer.render(<ToolbarFrame name='test' {...props}/>);
		return expect(
			renderer,
			'to have rendered',
			<div className='page'>
				<TestComponent/>
			</div>
		);
	});
});
