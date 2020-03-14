<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="text" indent="yes"/>
	<xsl:template match="/">
      <xsl:text>id	parent	name	tag</xsl:text>
      <xsl:value-of select="'&#13;'" />
      <xsl:apply-templates select="text" />
   </xsl:template>
	<xsl:template match="text">
		<xsl:value-of select="abbotation/text/@id"/>
	</xsl:template>
</xsl:stylesheet>
