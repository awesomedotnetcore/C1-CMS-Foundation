import expect from 'unittest/helpers/expect.js';
import React from 'react';
import TestUtils from 'react-addons-test-utils';
import Toolbar from 'console/components/presentation/Toolbar.js';
import ActionButton from 'console/components/presentation/ActionButton.js';

describe('Toolbar', () => {
	let renderer, props, actions;
	beforeEach(() => {
		actions = {
			first: () => {},
			second: () => {},
			third: () => {}
		};
		renderer = TestUtils.createRenderer();
		props = {
			name: 'toolbar',
			canSave: false,
			items: {
				first: {
					type: 'button',
					label: 'Label1',
					icon: 'save',
					action: actions.first
				},
				second: {
					type: 'button',
					label: 'Label2',
					action: actions.second
				},
				third: {
					type: 'button',
					label: 'Label3',
					saveButton: true,
					action: actions.third
				}
			}
		};
	});

	it('renders buttons', () => {
		renderer.render(<Toolbar {...props}/>);
		return expect(renderer, 'to have rendered',
			<div className='toolbar'>
				<ActionButton label='Label1' action={actions.first} icon='save'/>
				<ActionButton label='Label2' action={actions.second}/>
				<ActionButton label='Label3' action={actions.third} disabled={true}/>
			</div>
		);
	});

	it('does not render buttons where action is missing', () => {
		delete props.items.third.action;
		renderer.render(<Toolbar {...props}/>);
		return expect(renderer, 'to have rendered',
			<div className='toolbar'>
				<ActionButton label='Label1' action={actions.first} icon='save'/>
				<ActionButton label='Label2' action={actions.second}/>
			</div>
		);
	});

	it('renders buttons where label is missing, but only if it has icon', () => {
		delete props.items.first.label;
		delete props.items.third.label;
		renderer.render(<Toolbar {...props}/>);
		return expect(renderer, 'to have rendered',
			<div className='toolbar'>
				<ActionButton action={actions.first} icon='save'/>
				<ActionButton label='Label2' action={actions.second}/>
			</div>
		);
	});

	describe('styles', () => {
		it('sets classNames according to style list', () => {
			props.style = 'light rightAligned';
			renderer.render(<Toolbar {...props}/>);
			return expect(renderer, 'to have rendered',
				<div className='toolbar light rightAligned'>
					<ActionButton label='Label1' action={actions.first} icon='save'/>
					<ActionButton label='Label2' action={actions.second}/>
					<ActionButton label='Label3' action={actions.third} disabled={true}/>
				</div>
			);
		});
	});
});
