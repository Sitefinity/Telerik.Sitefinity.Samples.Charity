<div id="LoadingPanel" style="width: 100%; height: 100%; position: absolute; top: 0; left: 0; background: #fff; display: none; z-index: 99999; opacity: .7;">&nbsp;</div>
<div class="sfContentViews">
	<div id="WidgetOptions">
		<div>
			<div id="groupSettingPageSelect">
				<ul class="sfTargetList">
					<li>
						<label for="Location" class="sfTxtLbl">
							Company Name</label>
						<input type="text" id="Location" class="sfTxt" />
					</li>
					<li>
						<label for="Street" class="sfTxtLbl">
							Street Address</label>
						<input type="text" id="Street" class="sfTxt" />
					</li>
					<li>
						<label for="City" class="sfTxtLbl">
							City</label>
						<input type="text" id="City" class="sfTxt" />
					</li>
					<li>
						<label for="State" class="sfTxtLbl">
							State</label>
						<input type="text" id="State" class="sfTxt" />
					</li>
					<li>
						<label for="Zipcode" class="sfTxtLbl">
							Zipcode</label>
						<input type="text" id="Zipcode" class="sfTxt" />
					</li>
					<li>
						<input type="button" id="Geocode" value="Geocode" />
					</li>
					<li>
						<label for="Latitude" class="sfTxtLbl">
							Latitude</label>
						<input type="text" id="Latitude" class="sfTxt" />
					</li>
					<li>
						<label for="Longitude" class="sfTxtLbl">
							Longitude</label>
						<input type="text" id="Longitude" class="sfTxt" />
					</li>
				</ul>
			</div>

			<div>
				<ul>
					<li>
						<label for="Width" class="sfTxtLbl">
							Map Width</label>
						<input type="text" id="Width" class="sfTxt" />
					</li>
					<li>
						<label for="Height" class="sfTxtLbl">
							Map Height</label>
						<input type="text" id="Height" class="sfTxt" />
					</li>
					<li>
						<label for="Zoom" class="sfTxtLbl">
							Zoom Level</label>
						<select id="Zoom">
							<option value="1">1 - Min</option>
							<option value="2">2</option>
							<option value="3">3</option>
							<option value="4">4</option>
							<option value="5">5</option>
							<option value="6">6</option>
							<option value="7">7</option>
							<option value="8">8</option>
							<option value="9">9</option>
							<option value="10">10</option>
							<option value="11">11</option>
							<option value="12">12</option>
							<option value="13">13</option>
							<option value="14">14</option>
							<option value="15">15</option>
							<option value="16">16 - Max</option>
						</select>
					</li>
					<li>
						<label for="DashboardSize" class="sfTxtLbl">Map Controls</label>
						<select id="DashboardSize">
							<option value="Normal">Normal</option>
							<option value="Small">Small</option>
							<option value="Tiny">Tiny</option>
						</select>
					</li>
				</ul>
			</div>
		</div>
	</div>
</div>


<div id="hiddenmap" style="visibility: hidden; display: none;"></div>