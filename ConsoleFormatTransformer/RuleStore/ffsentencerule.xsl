<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="text" indent="yes"/>
	<xsl:strip-space elements="*"/>
	<xsl:template match="/">
      <xsl:text>id&#009;source</xsl:text>
	  <xsl:value-of select="'&#13;'"/>
      <xsl:apply-templates select="annotation/text/paragraphs/paragraph" />
   </xsl:template>
	<xsl:template match="sentence">
		<xsl:value-of select="concat(@id, '&#009;', source, '&#13;')"/>
	</xsl:template>
	
</xsl:stylesheet>